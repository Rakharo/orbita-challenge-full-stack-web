import { clientApi } from "@/plugins/clientApi";

export async function getAllStudents(pageNumber: number, pageSize: number) {
    try {
        const response = await clientApi.get(`Students?pageNumber=${pageNumber}&pageSize=${pageSize}`)

        return response.data;
    } catch(error: unknown) {
        console.error(error)
    }
}