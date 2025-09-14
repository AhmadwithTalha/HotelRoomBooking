import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReservationService } from '../../services/reservation.service';
import { Reservation } from '../../models/reservation.model';

@Component({
  selector: 'app-reservation-create',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './reservation-create.component.html',
  styleUrls: ['./reservation-create.component.css']
})
export class ReservationCreateComponent implements OnInit {
  form: FormGroup;
  availabilityMessage = '';
  conflictReservation?: Reservation;
  creating = false;

  constructor(private fb: FormBuilder, private reservationService: ReservationService) {
    this.form = this.fb.group({
      roomId: [null, Validators.required],
      guestName: ['', Validators.required],
      checkInDate: ['', Validators.required],
      checkOutDate: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  get roomId() { return this.form.get('roomId') as FormControl; }
  get guestName() { return this.form.get('guestName') as FormControl; }
  get checkInDate() { return this.form.get('checkInDate') as FormControl; }
  get checkOutDate() { return this.form.get('checkOutDate') as FormControl; }

  checkAvailability() {
    if (this.form.invalid) return;
    const { roomId, checkInDate, checkOutDate } = this.form.value;

    this.reservationService.checkAvailability(roomId, checkInDate, checkOutDate)
      .subscribe(res => {
        if (res.available) {
          this.availabilityMessage = 'Room is available!';
          this.conflictReservation = undefined;
        } else {
          this.availabilityMessage = 'Room is already booked!';
          this.conflictReservation = res.reservation;
        }
      });
  }

  createReservation() {
    if (this.form.invalid) return;
    if (this.conflictReservation) {
      alert('Cannot book. Room is already reserved!');
      return;
    }

    const { roomId, guestName, checkInDate, checkOutDate } = this.form.value;
    const reservation: Omit<Reservation, 'id' | 'createdAt'> = { roomId, guestName, checkInDate, checkOutDate };

    this.creating = true;
    this.reservationService.createReservation(reservation)
      .subscribe({
        next: () => {
          this.availabilityMessage = 'Reservation created successfully!';
          this.form.reset();
          this.creating = false;
        },
        error: err => {
          alert('Error: ' + err.error);
          this.creating = false;
        }
      });
  }
}
