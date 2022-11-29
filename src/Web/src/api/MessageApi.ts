import { AxiosInstance } from 'axios';
import { MessageForTypeScript } from '~/models';

export interface IMessageApi {
    getAll(): Promise<MessageForTypeScript[]>;
    get(messageId: number): Promise<MessageForTypeScript>;
    update(params: MessageForTypeScript): Promise<void>;
}

export class MessageApiForJson implements IMessageApi {
    constructor(public axios: AxiosInstance) { }

    async getAll(): Promise<MessageForTypeScript[]> {
        const startTime = performance.now();
        const response = await this.axios.get(`messages`);
        const endTime = performance.now();
        console.log(`[JSON]getMessages: ${Math.floor(endTime - startTime)}ms`);

        return response.data as MessageForTypeScript[];
    }

    async get(messageId: number): Promise<MessageForTypeScript> {
        const startTime = performance.now();
        const response = await this.axios.get(`messages/${messageId}`);
        const endTime = performance.now();
        console.log(`[JSON]getMessages/${messageId}: ${Math.floor(endTime - startTime)}ms`);
        
        return response.data as MessageForTypeScript;
    }
    
    async update (params: MessageForTypeScript): Promise<void> {
        await this.axios.put('messages', params);
    }
}

export class MessageApiForMemoryPack implements IMessageApi {
    constructor(public axios: AxiosInstance) { }

    async getAll(): Promise<MessageForTypeScript[]> {
        const startTime = performance.now();
        const response = await this.axios.get(`messages`, { responseType: 'arraybuffer' });
        let payload = MessageForTypeScript.deserializeArray(response.data);
        const endTime = performance.now();
        console.log(`[MemoryPack]getMessages: ${Math.floor(endTime - startTime)}ms`);

        return payload === null ? [] : payload!.map(x => x!) ;
    }

    async get(messageId: number): Promise<MessageForTypeScript> {
        const startTime = performance.now();
        const response = await this.axios.get(`messages/${messageId}`, { responseType: 'arraybuffer' });
        let message = MessageForTypeScript.deserialize(response.data)!;
        const endTime = performance.now();
        console.log(`[MemoryPack]getPerson/${messageId}: ${Math.floor(endTime - startTime)}ms`);
        
        return message;
    }

    async update (params: MessageForTypeScript): Promise<void> {
        var payload = MessageForTypeScript.serialize(params);
        await this.axios.put('messages', payload);
    }
}