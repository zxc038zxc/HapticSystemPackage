# Unity震動系統

讓 Unity 調用 Android, iOS 震動API

## 

打包成UnityPackage，只要使用git即可直接抓取至專案內
包括 Android, iOS的可控制時間、強度的震動系統架構

## 安裝
使用UnityPackage
```bash
https://github.com/zxc038zxc/HapticSystemPackage.git
```
## 使用說明
### Initialize
![image](https://github.com/user-attachments/assets/1e3d677a-ddfd-4a7d-aa53-470f221f99cc)
- iOS: 根據版本初始化
- Android: 呼叫API檢查"支援震動"
- ![image](https://github.com/user-attachments/assets/b54423e6-6944-458a-8fc2-6e9d4e0d4640)

### Deinitialize
![image](https://github.com/user-attachments/assets/c7bd4332-4189-4b96-87cf-57d08759c96c)

### Call
根據不同版本對應震動的功能，若都不支援則使用最基本的 'Handheld.Vibrate'
![image](https://github.com/user-attachments/assets/3ea2ba10-291e-483f-80e5-579836a8c851)

### Stop
![image](https://github.com/user-attachments/assets/edda5cb4-7684-4c36-b113-7cbdfff11490)

### Compute iOS version
![image](https://github.com/user-attachments/assets/614a923b-ec2c-4303-96ae-c112ac418993)


