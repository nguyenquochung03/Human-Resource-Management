import Bonus from '@/models/bonus';
import axiosConfig from '../config/axiosConfig';

class BonusService {
    add(payload: Bonus): Promise<any> {
        return axiosConfig.post('/api/Thuong', payload);
    }

    addBonusForEmployee(payload: any): Promise<any> {
        return axiosConfig.post('/api/Thuong/add', payload);
    }

    edit(id: string, payload: Bonus): Promise<any> {
        return axiosConfig.put('/api/Thuong/' + id, payload);
    }


    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/Thuong/' + id);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/Thuong');
    }

    findAllByEmployeeId(id: string) : Promise<any> {
        return axiosConfig.get('/api/Thuong/nhanvien/' + id);
    }

    getByPage(page:number, pageSize:number) : Promise<any>{
        return axiosConfig.get('/api/Thuong/pagination?page=' + page + '&pageSize=' + pageSize);
    }
}

const bonusService = new BonusService();
export default bonusService;