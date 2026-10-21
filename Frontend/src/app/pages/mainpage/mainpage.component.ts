import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-mainpage',
  standalone: true,
  templateUrl: './mainpage.component.html',
})
export class MainpageComponent implements OnInit {

  // NHÚNG ROUTER VÀO ĐỂ ĐIỀU HƯỚNG TỰ ĐỘNG
  constructor(private router: Router) {}

  ngOnInit(): void {
    // NGAY KHI VỪA VÀO TRANG MAIN, TỰ ĐỘNG CHUYỂN HƯỚNG THẲNG SANG TRANG KỶ NIỆM
    this.router.navigate(['/kyniem']);
  }
}