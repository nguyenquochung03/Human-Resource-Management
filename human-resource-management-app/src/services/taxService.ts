import Tax from '@/models/tax';
import axiosConfig from '../config/axiosConfig';
import formatDateForInput from '@/utils/formatDateInput';

class TaxService {
    add(payload: Tax): Promise<any> {
        return axiosConfig.post('/api/Thue', payload);
    }

    edit(id: string, payload: Tax): Promise<any> {
        return axiosConfig.put('/api/Thue/' + id, payload);
    }

    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/Thue/' + id);
    }

    findByEmployeeAndTime(employeeId: string, month: number, year: number) : Promise<any> {
        return axiosConfig.get('/api/Thue/' + employeeId + '/thoigian?thang='+month + '&nam=' + year);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/Thue');
    }

    findAllByEmployeeId(id: string) : Promise<any> {
        return axiosConfig.get('/api/Thue/MaNhanVien?id=' + id);
    }
    getByPage(page:number, pageSize:number, employeeName: string = "", filterTime: Date = new Date()) : Promise<any>{
        const params = new URLSearchParams();
        params.append('page', page.toString());
        params.append('pageSize', pageSize.toString());

        if (employeeName !== null && employeeName !== '') {
            params.append('employeeId', employeeName);
        }

        if (filterTime !== null) {
            params.append('filterTime', formatDateForInput(filterTime.toString()));
        }

        return axiosConfig.get('/api/Thue/pagination?' + params.toString());
    }
}

const taxService = new TaxService();
export default taxService;