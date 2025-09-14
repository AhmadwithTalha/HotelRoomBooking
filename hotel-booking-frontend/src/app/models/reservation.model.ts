export interface Reservation {
  id?: number; 
  roomId: number;
  guestName: string;
  checkInDate: string;
  checkOutDate: string;
  createdAt?: string;
}

export interface CheckAvailabilityResponse {
  available: boolean;
  reservation?: Reservation;
}
