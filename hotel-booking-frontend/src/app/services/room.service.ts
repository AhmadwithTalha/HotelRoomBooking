import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Room, RoomCreate } from '../models/room.model';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  private apiUrl = 'https://localhost:7251/api/Rooms';  // ✅ API base URL

  constructor(private http: HttpClient) {}

  // already working
  getRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(this.apiUrl);
  }

  // ✅ FIX: accept RoomCreate, return Room
  createRoom(room: RoomCreate): Observable<Room> {
    return this.http.post<Room>(this.apiUrl, room);
  }

  // ✅ update room by ID (needs Room because update requires id)
  updateRoom(id: number, room: Room): Observable<Room> {
    return this.http.put<Room>(`${this.apiUrl}/${id}`, room);
  }

  // ✅ delete room by ID
  deleteRoom(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
