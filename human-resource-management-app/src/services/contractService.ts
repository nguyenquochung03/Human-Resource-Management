import Contract from '@/models/contract';
import axiosConfig from '../config/axiosConfig';

class ContractService {
    add(payload: Contract): Promise<any> {
        return axiosConfig.post('/api/HopDong', payload);
    }

    edit(id: string, payload: Contract): Promise<any> {
        return axiosConfig.put('/api/HopDong/' + id, payload);
    }

    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/HopDong/' + id);
    }

    findById(id: string | undefined) : Promise<any> {
        return axiosConfig.get('/api/HopDong/' + id);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/HopDong');
    }

    findAllByEmployeeId(id: string) : Promise<any> {
        return axiosConfig.get('/api/HopDong/employeeId?employeeId=' + id);
    }
    getByPage(page:number, pageSize:number) : Promise<any>{
        return axiosConfig.get('/api/HopDong/pagination?page=' + page + '&pageSize=' + pageSize);
    }
}

const contractService = new ContractService();
export default contractService;