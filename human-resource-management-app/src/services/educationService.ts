import axiosConfig from '../config/axiosConfig';

class EducationService {
    add(payload: Education): Promise<any> {
        return axiosConfig.post('/api/TrinhDoHocVan', payload);
    }

    edit(id: string, payload: Education): Promise<any> {
        return axiosConfig.put('/api/TrinhDoHocVan/' + id, payload);
    }

    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/TrinhDoHocVan/' + id);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/TrinhDoHocVan');
    }

    
    findAllByEmployeeId(id: string) : Promise<any> {
        return axiosConfig.get('/api/TrinhDoHocVan/' + id);
    }

    getByPage(page:number, pageSize:number) : Promise<any>{
        return axiosConfig.get('/api/TrinhDoHocVan/pagination?page=' + page + '&pageSize=' + pageSize);
    }
}

const educationService = new EducationService();
export default educationService;