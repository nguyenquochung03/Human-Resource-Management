import Salary from '../models/Salary';
import axiosConfig from '../config/axiosConfig';
import formatDateForInput from '@/utils/formatDateInput';

class SalaryService {
    add(payload: Salary): Promise<any> {
        return axiosConfig.post('/api/Luong', payload);
    }

    edit(id: string, payload: Salary): Promise<any> {
        return axiosConfig.put('/api/Luong/' + id, payload);
    }

    findById(id: string): Promise<any> {
        return axiosConfig.get('/api/Luong/' + id);
    }

    computeSalary(payload: any) : Promise<any> {
        return axiosConfig.post('/api/Luong/Add-For-All', payload);
    }

    updateSalary(payload: any) : Promise<any> {
        return axiosConfig.put('/api/Luong/Update-For-All', payload);
    }

    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/Luong/' + id);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/Luong');
    }

    findAllByEmployeeId(id: string) : Promise<any> {
        return axiosConfig.get('/api/Luong/MaNhanVien?maNhanVien=' + id);
    }

    getByPage(page:number, pageSize:number, filterTime: Date = new Date(), employeeId: string = "") : Promise<any>{
        const params = new URLSearchParams();
        params.append('page', page.toString());
        params.append('pageSize', pageSize.toString());

        if (employeeId !== '') {
            params.append('maNhanVien', employeeId);
        }
      
        if (filterTime !== null) {
            params.append('month', formatDateForInput(filterTime.toString()));
        }

        return axiosConfig.get('/api/Luong/pagination?' + params.toString());
    }
}

const salaryService = new SalaryService();
export default salaryService;