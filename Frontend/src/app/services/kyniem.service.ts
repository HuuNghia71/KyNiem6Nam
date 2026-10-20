import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// Khớp với DistanceResponseDTO bên C# Backend
export interface DistanceResponseDTO {
  distanceKm: number;
  statusMessage: string;
}

// Khớp với UserSignalResponseDTO bên C# Backend
export interface UserSignalResponseDTO {
  userId: number;
  username: string;
  avatarUrl?: string;
  battery: string;
  weather: string;
  temperature: string;
}

@Injectable({
  providedIn: 'root'
})
export class KyniemService {
  // Trỏ tới controller KyNiem theo proxy.conf.json của bạn (/api/KyNiem)
  private api = '/api/KyNiem';

  constructor(private http: HttpClient) {}

  // 1. Gọi API lấy khoảng cách giữa hai người
  getDistance(): Observable<DistanceResponseDTO> {
    return this.http.get<DistanceResponseDTO>(`${this.api}/distance`);
  }

  // 2, 3, 4. Gọi API lấy thông tin tín hiệu (Pin, Thời tiết, Nhiệt độ) theo ID từng người
  getUserSignal(userId: number): Observable<UserSignalResponseDTO> {
    return this.http.get<UserSignalResponseDTO>(`${this.api}/signal/${userId}`);
  }
}