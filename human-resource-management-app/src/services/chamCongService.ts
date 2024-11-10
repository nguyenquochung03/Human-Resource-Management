import ChamCong from "@/models/chamCong";
import axiosConfig from "../config/axiosConfig";
import formatDateForInput from "@/utils/formatDateInput";

class ChamCongService {
    add(payload: ChamCong): Promise<any> {
        return axiosConfig.post("/api/ChamCong", payload);
    }

    edit(id: string, payload: ChamCong): Promise<any> {
        return axiosConfig.put("/api/ChamCong/" + id, payload);
    }

    delete(id: string | undefined): Promise<any> {
        return axiosConfig.delete("/api/ChamCong/" + id);
    }

    findAll(): Promise<any> {
        return axiosConfig.get("/api/ChamCong");
    }

    findAllByEmployeeId(id: string): Promise<any> {
        return axiosConfig.get("/api/ChamCong/MaNhanVien?id=" + id);
    }

    getByPage(page: number, pageSize: number, day: Date = new Date()): Promise<any> {
        return axiosConfig.get(
            "/api/ChamCong/pagination?page=" + page + "&pageSize=" + pageSize + "&day=" + formatDateForInput(day.toString())
        );
    }

    findAllByDay(day: string) : Promise<any> {
        return axiosConfig.get('/api/ChamCong/day?day=' + day);
    }

    findAllFromTo(employeeId: string | undefined, from: string, to: string) : Promise<any> {
        return axiosConfig.get('/api/ChamCong/day/' + employeeId + '?from=' + from + '&to=' + to);
    }

}

const chamCongService = new ChamCongService();
export default chamCongService;
