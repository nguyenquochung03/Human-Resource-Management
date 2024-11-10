import Department from '@/models/department';
import axiosConfig from '../config/axiosConfig';

class DepartmentService {
    add(payload: Department): Promise<any> {
        return axiosConfig.post('/api/PhongBan', payload);
    }

    edit(id: string, payload: Department): Promise<any> {
        return axiosConfig.put('/api/PhongBan/' + id, payload);
    }

    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/PhongBan/' + id);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/PhongBan');
    }

    getByPage(page:number, pageSize:number) : Promise<any>{
        return axiosConfig.get('/api/PhongBan/pagination?page=' + page + '&pageSize=' + pageSize);
    }
}

const departmentService = new DepartmentService();
export default departmentService;