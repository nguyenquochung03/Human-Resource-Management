import Position from '@/models/position';
import axiosConfig from '../config/axiosConfig';

class PositionService {
    add(payload: Position): Promise<any> {
        return axiosConfig.post('/api/ChucVu', payload);
    }

    edit(id: string, payload: Position): Promise<any> {
        return axiosConfig.put('/api/ChucVu/' + id, payload);
    }

    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/ChucVu/' + id);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/ChucVu');
    }

    findAllByDepartmentId(id: string) : Promise<any> {
        return axiosConfig.get('/api/ChucVu/phongBan/' + id);
    }

    getByPage(page: number, pageSize: number, phongBanId: string = "", chucVu: string = ""): Promise<any> {
        const params = new URLSearchParams();
        params.append('page', page.toString());
        params.append('pageSize', pageSize.toString());

        if (phongBanId !== '') {
            params.append('phongBanId', phongBanId);
        }

        if (chucVu !== '') {
            params.append('searchChucVu',chucVu);
        }

        return axiosConfig.get('/api/ChucVu/pagination?' + params.toString());
     
    }
}

const positionService = new PositionService();
export default positionService;