import PayrollPeriod from '@/models/payrollPeriod';
import axiosConfig from '../config/axiosConfig';
import formatDateForInput from '@/utils/formatDateInput';

class PayrollPeriodService {
    add(payload: PayrollPeriod): Promise<any> {
        return axiosConfig.post('/api/DotTraLuong', payload);
    }

    edit(id: string, payload: PayrollPeriod): Promise<any> {
        return axiosConfig.put('/api/DotTraLuong/' + id, payload);
    }

    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/DotTraLuong/' + id);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/DotTraLuong');
    }

    getByPage(page:number, pageSize:number, month: Date) : Promise<any>{
        const params = new URLSearchParams();
        params.append('page', page.toString());
        params.append('pageSize', pageSize.toString());

        if (month !== null) {
            params.append('month', formatDateForInput(month.toString()));
        }

        return axiosConfig.get('/api/DotTraLuong/pagination?' + params.toString());
    }
}

const payrollPeriodService = new PayrollPeriodService();
export default payrollPeriodService;