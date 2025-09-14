import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RoomService } from '../../services/room.service';
import { RoomCreate } from '../../models/room.model';

@Component({
  selector: 'app-room-create',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <h2>Add New Room</h2>
    <form (ngSubmit)="createRoom()" #f="ngForm">
      <label>Room Number:</label>
      <input name="roomNumber" [(ngModel)]="model.roomNumber" required />
      <br/>
      <label>Type:</label>
      <input name="type" [(ngModel)]="model.type" required />
      <br/>
      <label>Price Per Night:</label>
      <input type="number" name="pricePerNight" [(ngModel)]="model.pricePerNight" required />
      <br/>
      <button [disabled]="f.invalid" type="submit">Create Room</button>
    </form>
  `
})
export class RoomCreateComponent {
  model: RoomCreate = { roomNumber: '', type: '', pricePerNight: 0 };

  constructor(private roomService: RoomService) {}

  createRoom() {
    this.roomService.createRoom(this.model).subscribe({
      next: () => {
        alert(' Room created successfully!');
        // reset form
        this.model = { roomNumber: '', type: '', pricePerNight: 0 };
      },
     error: (err: any) => {
  console.error('Error creating room:', err);
}

    });
  }
}
