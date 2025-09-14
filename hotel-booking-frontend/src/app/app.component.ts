import { Component } from '@angular/core';
import { RoomCreateComponent } from './components/room-create/room-create.component';
import { RoomListComponent } from './components/room-list/room-list.component';
import { ReservationCreateComponent } from './components/reservation-create/reservation-create.component';
imports: [RoomCreateComponent, RoomListComponent, ReservationCreateComponent]

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RoomCreateComponent, RoomListComponent, ReservationCreateComponent],
  template: `
    <h1>Hotel Room Booking</h1>
    <app-room-create></app-room-create>
    <hr />
    <app-room-list></app-room-list>
    <hr />
    <h1>Check Availability and Make a Reservation</h1>
    <app-reservation-create></app-reservation-create>
  `
})
export class AppComponent {}
