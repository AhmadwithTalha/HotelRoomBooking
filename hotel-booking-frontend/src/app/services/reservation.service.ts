import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Reservation, CheckAvailabilityResponse } from '../models/reservation.model';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  private apiUrl = 'https://localhost:7251/api/Reservations';

  constructor(private http: HttpClient) {}

  checkAvailability(roomId: number, from: string, to: string): Observable<CheckAvailabilityResponse> {
    const params = new HttpParams()
      .set('roomId', roomId.toString())
      .set('from', from)
      .set('to', to);

    return this.http.get<CheckAvailabilityResponse>(`${this.apiUrl}/CheckAvailability`, { params });
  }

  createReservation(reservation: Omit<Reservation, 'id' | 'createdAt'>): Observable<Reservation> {
    return this.http.post<Reservation>(this.apiUrl, reservation);
  }
}
