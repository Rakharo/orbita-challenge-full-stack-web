import type { iStudent } from "@/interfaces/studentInterface";
import { clientApi } from "@/plugins/clientApi";

export async function getAllStudents(pageNumber: number, pageSize: number) {
    try {
        const response = await clientApi.get(`Students?pageNumber=${pageNumber}&pageSize=${pageSize}`)

        return response.data;
    } catch(error) {
        console.error(error)
    }
}

export async function createNewStudent(student: iStudent) {
    try {
        const response = await clientApi.post('Students', student)

        return response.data
    } catch(error: unknown) {
        console.error(error)
    }
}

export async function editStudent(ra: string, student: iStudent) {{
    try {
        const response = await clientApi.put(`Students/${ra}`, student)

        return response.data;
    } catch(error) {
        console.error(error)
    }
}}

export async function deleteStudent(ra: string) {
    try {
        const response = await clientApi.delete(`Students/${ra}`)

        return response.data
    } catch(error) {
        console.error(error)
    }
}