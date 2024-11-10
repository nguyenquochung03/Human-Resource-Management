import instance  from '../config/axiosConfig';

class UserService {
    getProfile() : Promise<any> {
        return instance.get('/api/TaiKhoan');
    }

    login(payload: Login): Promise<any> {
        return instance.post('/api/TaiKhoan/login', payload)
    }
}

const userService = new UserService();
export default userService;