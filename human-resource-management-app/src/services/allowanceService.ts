import Allowance from '@/models/allowance';
import axiosConfig from '../config/axiosConfig';

class AllowanceService {
    add(payload: Allowance): Promise<any> {
        return axiosConfig.post('/api/PhuCap', payload);
    }

    addAllowanceForEmployee(payload: any) : Promise<any> {
        return axiosConfig.post('/api/PhuCap/add', payload);
    }

    edit(id: string, payload: Allowance): Promise<any> {
        return axiosConfig.put('/api/PhuCap/' + id, payload);
    }

    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/PhuCap/' + id);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/PhuCap');
    }

    findAllByEmployeeId(id : string) : Promise<any> {
        return axiosConfig.get('/api/PhuCap/employee?employeeId=' + id);
    }
    getByPage(page:number, pageSize:number) : Promise<any>{
        return axiosConfig.get('/api/PhuCap/pagination?page=' + page + '&pageSize=' + pageSize);
    }
}

const allowanceService = new AllowanceService();
export default allowanceService;