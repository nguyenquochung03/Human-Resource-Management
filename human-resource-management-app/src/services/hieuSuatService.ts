import HieuSuat from '@/models/hieuSuat';
import axiosConfig from '../config/axiosConfig';

class HieuSuatService {
    add(payload: HieuSuat): Promise<any> {
        return axiosConfig.post('/api/HieuSuat', payload);
    }

    edit(id: string, payload: HieuSuat): Promise<any> {
        return axiosConfig.put('/api/HieuSuat/' + id, payload);
    }

    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/HieuSuat/' + id);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/HieuSuat');
    }

    findAllByEmployeeId(id: string) : Promise<any> {
        return axiosConfig.get('/api/HieuSuat/MaNhanVien?maNhanVien=' + id);
    }

    getByPage(page:number, pageSize:number) : Promise<any>{
        return axiosConfig.get('/api/HieuSuat/pagination?page=' + page + '&pageSize=' + pageSize);
    }
}

const hieuSuatService = new HieuSuatService();
export default hieuSuatService;