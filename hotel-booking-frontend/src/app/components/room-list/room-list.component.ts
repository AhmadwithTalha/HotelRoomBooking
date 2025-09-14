import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RoomService } from '../../services/room.service';
import { Room } from '../../models/room.model';

@Component({
  selector: 'app-room-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './room-list.component.html',
  styleUrls: ['./room-list.component.css']
})
export class RoomListComponent implements OnInit {
  rooms: Room[] = [];
  editRoomData: Room | null = null; // ✅ track which room is being edited

  constructor(private roomService: RoomService) {}

  ngOnInit(): void {
    this.loadRooms();
  }

  // already working
  loadRooms(): void {
    this.roomService.getRooms().subscribe({
      next: (data) => this.rooms = data,
      error: (err) => console.error('Error loading rooms:', err)
    });
  }

  // ✅ start editing
  startEdit(room: Room): void {
    this.editRoomData = { ...room }; // copy object
  }

  // ✅ save update
  saveUpdate(): void {
    if (this.editRoomData) {
      this.roomService.updateRoom(this.editRoomData.id, this.editRoomData).subscribe({
        next: () => {
          this.loadRooms();      // refresh list
          this.editRoomData = null;
        },
        error: (err) => console.error('Error updating room:', err)
      });
    }
  }

  // ✅ cancel editing
  cancelEdit(): void {
    this.editRoomData = null;
  }

  // ✅ delete room
  deleteRoom(id: number): void {
    if (confirm('Are you sure you want to delete this room?')) {
      this.roomService.deleteRoom(id).subscribe({
        next: () => {
          this.rooms = this.rooms.filter(r => r.id !== id); // remove from UI
        },
        error: (err) => console.error('Error deleting room:', err)
      });
    }
  }
}
