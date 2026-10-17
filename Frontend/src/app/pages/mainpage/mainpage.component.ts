import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-mainpage',
  standalone: true, // Vì bạn đang dùng chuẩn mới
  templateUrl: './mainpage.component.html',
})
export class MainpageComponent {

  // Nhúng Router vào để điều hướng
  constructor(private router: Router) {}

  // Hàm chuyển sang trang login khi bấm nút
  moKhoaBiMat() {
    this.router.navigate(['/login']);
  }
}