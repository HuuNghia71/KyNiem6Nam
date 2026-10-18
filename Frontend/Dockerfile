# Bước 1: Build Angular bằng Node.js
FROM node:20 AS build
WORKDIR /app

# Cài đặt thư viện và build
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build --configuration=production

# Bước 2: Bỏ code tĩnh vào Nginx để chạy
FROM nginx:alpine
# Copy file cấu hình nginx thay thế cho file mặc định
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copy thư mục build của Angular vào thư mục phục vụ của Nginx
# LƯU Ý: Chữ "anniversary-app" là tên project trong angular.json của bạn
COPY --from=build /app/dist/anniversary-app/browser /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]