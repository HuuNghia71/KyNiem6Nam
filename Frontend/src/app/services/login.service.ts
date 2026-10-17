import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface LoginRequestDTO {
  passcode: string;
}

export interface LoginResponseDTO {
  userId: number;
  username: string;
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  // Đường dẫn gọi xuống backend theo proxy.conf.json của bạn
  private api = '/api/Auth'; 

  constructor(private http: HttpClient) {}

  login(request: LoginRequestDTO): Observable<LoginResponseDTO> {
    return this.http.post<LoginResponseDTO>(`${this.api}/login`, request);
  }

  // Tiện ích để lưu trữ token sau khi đăng nhập thành công
  saveToken(token: string) {
    localStorage.setItem('kyniem_token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('kyniem_token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout() {
    localStorage.removeItem('kyniem_token');
    localStorage.removeItem('kyniem_userId');
    localStorage.removeItem('kyniem_username');
  }
}