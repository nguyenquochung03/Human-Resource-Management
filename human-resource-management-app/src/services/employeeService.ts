import axiosConfig from '../config/axiosConfig';

class EmployeeService { 
    add(payload: any): Promise<any> { 
        return axiosConfig.post('/api/NhanVien', payload); 
    }

    edit(id: string, payload: any): Promise<any> { 
        return axiosConfig.put('/api/NhanVien/' + id, payload); 
    }

    delete(id: string | undefined) : Promise<any> {
        return axiosConfig.delete('/api/NhanVien/' + id);
    }

    findAll() : Promise<any> {
        return axiosConfig.get('/api/NhanVien');
    }

    searchByName(keyword: string)  : Promise<any> {
        return axiosConfig.get('/api/NhanVien/get-by-name?name=' + keyword)
    }

    searchById(keyword: string) : Promise<any> {
        return axiosConfig.get('/api/NhanVien/get-by-id?id=' + keyword)
    }

    searchByPhone(keyword: string) : Promise<any> {
        return axiosConfig.get('/api/NhanVien/get-by-phone?phone=' + keyword)
    }

    getByPage(page:number, pageSize:number, queryType: string, queryValue: string) : Promise<any>{
        const params = new URLSearchParams();
        params.append('page', page.toString());
        params.append('pageSize', pageSize.toString());

        if (queryType !== '') {
            params.append('queryType', queryType);
        }

        if (queryValue !== '') {
            params.append('queryValue', queryValue);
        }

        return axiosConfig.get('/api/NhanVien/pagination?' + params.toString());
    }
    
}

const employeeService = new EmployeeService(); 
export default employeeService;
