import { AxiosInstance } from 'axios';
import { PersonForTypeScript } from '~/models';

export interface IPersonApi {
    getAll(): Promise<PersonForTypeScript[]>;
    get(personId: number): Promise<PersonForTypeScript>;
}

export class PersonApiForJson implements IPersonApi {
    constructor(public axios: AxiosInstance) { }

    async getAll(): Promise<PersonForTypeScript[]> {
        const startTime = performance.now();
        const response = await this.axios.get('persons');
        const endTime = performance.now();
        console.log(`[JSON]getPersons: ${Math.floor(endTime - startTime)}ms`);

        return response.data as PersonForTypeScript[];
    }

    async get(personId: number): Promise<PersonForTypeScript> {
        const startTime = performance.now();
        const response = await this.axios.get(`persons/${personId}`);
        const endTime = performance.now();
        console.log(`[JSON]getPerson/${personId}: ${Math.floor(endTime - startTime)}ms`);

        return response.data as PersonForTypeScript;
    }
}

export class PersonApiForMemoryPack implements IPersonApi {
    constructor(public axios: AxiosInstance) { }

    async getAll(): Promise<PersonForTypeScript[]> {
        const startTime = performance.now();
        const response = await this.axios.get('persons', { responseType: 'arraybuffer' });
        let payload = PersonForTypeScript.deserializeArray(response.data);
        const endTime = performance.now();
        console.log(`[MemoryPack]getPersons: ${Math.floor(endTime - startTime)}ms`);

        return payload === null ? [] : payload!.map(x => x!) ;
    }

    async get(personId: number): Promise<PersonForTypeScript> {
        const startTime = performance.now();
        const response = await this.axios.get(`persons/${personId}`, { responseType: 'arraybuffer' });
        let person = PersonForTypeScript.deserialize(response.data)!;
        const endTime = performance.now();
        console.log(`[MemoryPack]getPerson/${personId}: ${Math.floor(endTime - startTime)}ms`);

        return person;
    }
}