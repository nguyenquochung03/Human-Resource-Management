import formatDateForInput from '@/utils/formatDateInput';
import axiosConfig from '../config/axiosConfig';

class AbsenceService {
    add(payload: Absence): Promise<any> {
        return axiosConfig.post('/api/NghiVang', payload);
    }

    edit(id: string, payload: Absence): Promise<any> {
        return axiosConfig.put('/api/NghiVang/' + id, payload);
    }

    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/NghiVang/' + id);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/NghiVang');
    }

    findAllByEmployeeId(id: string) : Promise<any> {
        return axiosConfig.get('/api/NghiVang/MaNhanVien?id=' + id);
    }

    findAllByMonth(id: string, month: Date) : Promise<any> {
        return axiosConfig.get('/api/NghiVang/' + id + '/month?month=' + formatDateForInput(month.toString()));
    }

    getByPage(page:number, pageSize:number) : Promise<any>{
        return axiosConfig.get('/api/NghiVang/pagination?page=' + page + '&pageSize=' + pageSize);
    }
}

const absenceService = new AbsenceService();
export default absenceService;