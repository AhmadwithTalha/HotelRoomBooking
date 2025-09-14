export interface Room {
  id: number;
  roomNumber: string;
  type: string;
  pricePerNight: number;
}


export interface RoomCreate {
  roomNumber: string;
  type: string;
  pricePerNight: number;
}

export interface Reservation {
  id?: number;          // ✅ ensure ID exists
  roomId: number;
  guestName: string;
  checkInDate: string;
  checkOutDate: string;
  createdAt?: string;
}
