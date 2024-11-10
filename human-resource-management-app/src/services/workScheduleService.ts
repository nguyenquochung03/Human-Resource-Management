import WorkSchedule from '@/models/workSchedule';
import axiosConfig from '../config/axiosConfig';
import formatDateForInput from '@/utils/formatDateInput';

class WorkScheduleService {
    add(payload: WorkSchedule): Promise<any> {
        return axiosConfig.post('/api/LichLam', payload);
    }

    addScheduleForEmployee(payload: any): Promise<any> {
        return axiosConfig.post('/api/LichLam/add', payload);
    }

    edit(id: string, payload: WorkSchedule): Promise<any> {
        return axiosConfig.put('/api/LichLam/' + id, payload);
    }

    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/LichLam/' + id);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/LichLam');
    }

    findAllByEmployeeId(id: string) : Promise<any> {
        return axiosConfig.get('/api/LichLam/nhanvien/' + id);
    }

    getByPage(page:number, pageSize:number) : Promise<any>{
        return axiosConfig.get('/api/LichLam/pagination?page=' + page + '&pageSize=' + pageSize);
    }

    findAllFromTo(from: Date, to: Date) : Promise<any> {
        return axiosConfig.get('/api/LichLam/day' + '?from=' + formatDateForInput(from.toString()) + '&to=' + formatDateForInput(to.toString()));
    }
}

const workScheduleService = new WorkScheduleService();
export default workScheduleService;