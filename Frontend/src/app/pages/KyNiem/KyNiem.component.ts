import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { KyniemService, DistanceResponseDTO, UserSignalResponseDTO } from '../../services/kyniem.service';

// KHAI BÁO CẤU TRÚC DỮ LIỆU CỦA 1 CỘT MỐC
interface Milestone {
  id: string;
  dateLabel: string;
  title: string;
  description: string;
  images: string[];
}

@Component({
  selector: 'app-kyniem',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './kyniem.html',
  styleUrls: ['./kyniem.css']
})
export class KyniemComponent implements OnInit, OnDestroy {

  distanceInfo?: DistanceResponseDTO;
  hoangSignal?: UserSignalResponseDTO;
  maiSignal?: UserSignalResponseDTO;

  days: number = 0;
  months: number = 0;
  hours: number = 0;
  minutes: number = 0;
  today: Date = new Date(); 
  private timer: any; 
  private slideshowTimer: any; // Bộ đếm cho trình chiếu ảnh

  // DANH SÁCH 8 CỘT MỐC VÀ ĐƯỜNG DẪN ẢNH TƯƠNG ỨNG
  milestones: Milestone[] = [
    {
      id: '102020', dateLabel: '10/2020', title: 'Quen Nhau Trong Lớp',
      description: 'Lần đầu tiên gặp gỡ trong lớp học. Những ánh mắt chạm nhau ngập ngừng đã mở ra khởi đầu cho một chuyện tình thanh xuân rực rỡ nhất.',
      images: ['Anh/102020/1.jpg', 'Anh/102020/2.jpg', 'Anh/102020/3.jpg']
    },
    {
      id: '052022', dateLabel: '05/2022', title: 'Lần Đầu Đi Đà Lạt Chung',
      description: 'Chuyến đi xa đầu tiên của hai đứa. Cùng nhau trốn cái nóng Sài Gòn, đón chút se lạnh mộng mơ và viết nên những kỷ niệm khó quên.',
      images: ['Anh/052022/1.jpg', 'Anh/052022/2.jpg', 'Anh/052022/3.jpg', 'Anh/052022/4.jpg', 'Anh/052022/5.jpg', 'Anh/052022/6.jpg']
    },
    {
      id: '062022', dateLabel: '06/2022', title: 'Cùng Nhau Tốt Nghiệp Cấp 3',
      description: 'Khép lại tuổi học trò bồng bột. Cùng nhau mặc áo cử nhân, cầm tấm bằng và hứa hẹn sẽ nắm tay nhau bước tiếp quãng đường đại học.',
      images: ['Anh/062022/1.jpg']
    },
    {
      id: '122023', dateLabel: '12/2023', title: 'Lụm Được "Đứa Con Rơi"',
      description: 'Gia đình nhỏ chính thức đón thêm một bé mèo đáng yêu. Tình yêu nhân đôi, tiếng cười ngập tràn khi cả hai cùng làm "bố mẹ trẻ".',
      images: ['Anh/122023/1.jpg']
    },
    {
      id: '052024', dateLabel: '05/2024', title: 'Một Ngày Trọn Vẹn Cùng Hai Mẹ Con',
      description: 'Dành trọn vẹn một ngày cuối tuần rong ruổi đi chơi. Nhìn em cười hạnh phúc bên "con", anh thấy thế giới của mình bình yên đến lạ.',
      images: ['Anh/052024/1.jpg']
    },
    {
      id: '052025', dateLabel: '05/2025', title: 'Vũng Tàu Lần Thứ "n"',
      description: 'Lại xách balo lên và đi biển Vũng Tàu. Nơi này đến không biết bao nhiêu lần, nhưng lần nào đi cùng em cũng bồi hồi như ngày mới yêu.',
      images: ['Anh/052025/1.jpg']
    },
    {
      id: '092025', dateLabel: '09/2025', title: 'Đà Lạt Vẫy Gọi Tiếp',
      description: 'Trở lại Đà Lạt mộng mơ. Vẫn là cái lạnh ấy, vẫn là những cung đường ấy, và quan trọng nhất, vẫn là đôi bàn tay ấy nắm chặt lấy nhau.',
      images: ['Anh/092025/1.jpg']
    },
    {
      id: '042026', dateLabel: '04/2026', title: 'Thảo Cầm Viên Ký Sự',
      description: 'Chuyến dạo chơi Thảo Cầm Viên lần thứ n. Vẫn háo hức ngắm nhìn muông thú như những đứa trẻ, trân trọng từng giây phút êm đềm bên em.',
      images: ['Anh/042026/1.jpg']
    }
  ];

  // BIẾN QUẢN LÝ ẢNH ĐANG HIỂN THỊ
  activeMilestone: Milestone = this.milestones[0];
  currentImageIndex: number = 0;

  constructor(
    private kyniemService: KyniemService,
    private cdr: ChangeDetectorRef 
  ) {}

  ngOnInit(): void {
    this.calculateTime();
    this.startSlideshow(); // Bắt đầu tự động chuyển ảnh

    this.timer = setInterval(() => {
      this.calculateTime();
      this.cdr.detectChanges(); 
    }, 60000);

    this.kyniemService.getDistance().subscribe({
      next: (res) => { this.distanceInfo = res; this.cdr.detectChanges(); },
      error: (err) => { console.error('LỖI KHOẢNG CÁCH', err); }
    });

    this.kyniemService.getUserSignal(1).subscribe({
      next: (res) => { this.hoangSignal = res; this.cdr.detectChanges(); },
      error: (err) => { console.error('LỖI HOÀNG', err); }
    });

    this.kyniemService.getUserSignal(2).subscribe({
      next: (res) => { this.maiSignal = res; this.cdr.detectChanges(); },
      error: (err) => { console.error('LỖI MAI', err); }
    });
  }

  ngOnDestroy(): void {
    if (this.timer) clearInterval(this.timer);
    if (this.slideshowTimer) clearInterval(this.slideshowTimer);
  }

  // --- LOGIC XỬ LÝ ẢNH & CỘT MỐC ---

  // Khi người dùng bấm vào một cột mốc
  selectMilestone(m: Milestone): void {
    this.activeMilestone = m;
    this.currentImageIndex = 0; // Trở về ảnh đầu tiên của thư mục
    this.startSlideshow(); // Khởi động lại vòng lặp chuyển ảnh
  }

  // Bấm nút Qua Phải
  nextImage(): void {
    if (this.activeMilestone && this.activeMilestone.images.length > 0) {
      this.currentImageIndex = (this.currentImageIndex + 1) % this.activeMilestone.images.length;
    }
  }

  // Bấm nút Qua Trái
  prevImage(): void {
    if (this.activeMilestone && this.activeMilestone.images.length > 0) {
      this.currentImageIndex = (this.currentImageIndex - 1 + this.activeMilestone.images.length) % this.activeMilestone.images.length;
    }
  }

  // Hẹn giờ tự động lướt ảnh (Mỗi 3 giây)
  startSlideshow(): void {
    if (this.slideshowTimer) clearInterval(this.slideshowTimer);
    this.slideshowTimer = setInterval(() => {
      this.nextImage();
      this.cdr.detectChanges();
    }, 3000);
  }

  // Nếu lỡ cột mốc chưa có ảnh, hiển thị ảnh mặc định siêu đẹp thay vì báo lỗi vỡ ảnh
  onImageError(event: any): void {
    event.target.src = 'https://images.unsplash.com/photo-1518199266791-5375a83190b7?q=80&w=800&auto=format&fit=crop';
  }

  calculateTime(): void {
    const startDate = new Date('2020-10-24T00:00:00');
    const now = new Date(); 
    const diffTime = Math.abs(now.getTime() - startDate.getTime());

    this.minutes = Math.floor(diffTime / (1000 * 60));
    this.hours = Math.floor(diffTime / (1000 * 60 * 60));
    this.days = Math.floor(diffTime / (1000 * 60 * 60 * 24));

    let months = (now.getFullYear() - startDate.getFullYear()) * 12;
    months -= startDate.getMonth();
    months += now.getMonth();
    if (now.getDate() < startDate.getDate()) { months--; }
    this.months = months;
  }
}