import { Routes } from '@angular/router';
import { MainpageComponent } from './pages/mainpage/mainpage.component';
import { LoginComponent } from './pages/login/login.component';
import { KyniemComponent } from './pages/KyNiem/KyNiem.component'; // <-- Nhớ import component kỷ niệm vào đây (đường dẫn có thể thay đổi tùy thư mục của bạn)

export const routes: Routes = [
  // Tự động chuyển hướng sang /main khi vào trang gốc
  // { path: '', redirectTo: 'main', pathMatch: 'full' },
  { path: '', component: KyniemComponent },
  { path: 'main', component: MainpageComponent },
  { path: 'login', component: LoginComponent },
  
  { path: 'kyniem', component: KyniemComponent }
];