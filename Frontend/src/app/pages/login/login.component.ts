import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LoginService } from '../../services/login.service';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})
export class LoginComponent {
  passcode: string = '';
  maxLength: number = 6;
  errorMessage: string = '';
  isErrorShake: boolean = false; // Dùng để tạo hiệu ứng rung khi nhập sai

  // Mảng dùng để vẽ 6 dấu chấm trên màn hình
  dots = [1, 2, 3, 4, 5, 6]; 

  constructor(private loginService: LoginService, private router: Router) {}

  appendNumber(num: number) {
    if (this.passcode.length < this.maxLength) {
      this.passcode += num;
      this.errorMessage = ''; // Xóa câu thông báo lỗi khi bắt đầu nhập lại

      // Khi nhập đủ 6 số thì tự động gửi API đăng nhập
      if (this.passcode.length === this.maxLength) {
        this.submitLogin();
      }
    }
  }

  removeNumber() {
    if (this.passcode.length > 0) {
      this.passcode = this.passcode.slice(0, -1);
      this.errorMessage = '';
    }
  }

  submitLogin() {
    this.loginService.login({ passcode: this.passcode }).subscribe({
      next: (res) => {
        // Đăng nhập thành công -> Lưu dữ liệu
        this.loginService.saveToken(res.token);
        localStorage.setItem('kyniem_userId', res.userId.toString());
        localStorage.setItem('kyniem_username', res.username);
        
        // Điều hướng vào trang chủ hoặc trang bản đồ
        this.router.navigate(['/']); 
      },
      error: (err) => {
        // Sai Passcode -> Hiển thị lỗi, xóa ô nhập và rung nhẹ
        this.errorMessage = err.error?.message || 'Mã PIN không chính xác!';
        this.passcode = ''; 
        this.triggerShake();
      }
    });
  }

  // Kích hoạt hiệu ứng CSS rung
  triggerShake() {
    this.isErrorShake = true;
    setTimeout(() => {
      this.isErrorShake = false;
    }, 500); 
  }
}