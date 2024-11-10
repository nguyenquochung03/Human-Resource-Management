import axiosConfig from '../config/axiosConfig';
import formatDateForInput from '@/utils/formatDateInput';

class SalaryPaymentService {
    add(payload: SalaryPayment): Promise<any> {
        return axiosConfig.post('/api/TraLuong', payload);
    }

    edit(id: string, payload: SalaryPayment): Promise<any> {
        return axiosConfig.put('/api/TraLuong/' + id, payload);
    }

    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/TraLuong/' + id);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/TraLuong');
    }

    findById(payrollPeriodId: string, employeeId: string) : Promise<any> {
        return axiosConfig.get('/api/TraLuong/' + payrollPeriodId + "/" + employeeId);
    }

    findAllByEmployeeId(id: string) : Promise<any> {
        return axiosConfig.get('/api/TraLuong/MaNhanVien?id=' + id);
    }
    getByPage(page:number, pageSize:number, employeeName: string = "", month: Date = new Date()) : Promise<any>{
        const params = new URLSearchParams();
        params.append('page', page.toString());
        params.append('pageSize', pageSize.toString());

        if (employeeName !== '') {
            params.append('employeeName', employeeName);
        }

        params.append('month', formatDateForInput(month.toString()));
      
        return axiosConfig.get('/api/TraLuong/pagination?' + params.toString());
    }
}

const salaryPaymentService = new SalaryPaymentService();
export default salaryPaymentService;