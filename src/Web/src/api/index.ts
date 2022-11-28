import { IMessageApi, MessageApiForJson, MessageApiForMemoryPack } from './MessageApi';
import { IPersonApi, PersonApiForJson, PersonApiForMemoryPack } from './PersonApi';

let messageApiForJson: IMessageApi;
let messageApiForMemoryPack: IMessageApi;
let personApiForJson: IPersonApi;
let personApiForMemoryPack: IPersonApi;

export function initializeGateway(axiosForJson, axiosForMemoryPack) {
    messageApiForJson = new MessageApiForJson(axiosForJson);
    messageApiForMemoryPack = new MessageApiForMemoryPack(axiosForMemoryPack);
    personApiForJson = new PersonApiForJson(axiosForJson);
    personApiForMemoryPack = new PersonApiForMemoryPack(axiosForMemoryPack);
}

export {
    messageApiForJson,
    messageApiForMemoryPack,
    personApiForJson,
    personApiForMemoryPack
};