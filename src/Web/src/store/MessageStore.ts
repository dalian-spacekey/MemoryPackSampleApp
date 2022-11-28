import { Module, VuexModule, Mutation, Action } from 'vuex-module-decorators';
import { messageApiForJson, messageApiForMemoryPack } from '~/api';
import { MessageForTypeScript } from '~/models';
import * as utilities from '~/utilities';

@Module({
    name: 'MessageStore',
    stateFactory: true,
    namespaced: true
})
export default class MessageStore extends VuexModule {
    messages: MessageForTypeScript[] = [];
    message: MessageForTypeScript | null = null;
    @Mutation
    setMessages(messages: MessageForTypeScript[]) {
        this.messages = messages;
    }

    @Mutation
    setMessage(message: MessageForTypeScript) {
        this.message = message;
    }

    @Action
    async getMessagesJson() {
        const data = await messageApiForJson.getAll();

        if (data) {
            this.setMessages(data);
        }
    }

    @Action
    async getMessageJson(payload: { messageId: number }) {

        let m = await messageApiForJson.get(payload.messageId);
        if (!m) throw Error();

        this.setMessage(m);
    }

    @Action
    async getMessagesMemoryPack() {
        const data = await messageApiForMemoryPack.getAll();

        if (data) {
            this.setMessages(data);
        }
    }

    @Action
    async getMessageMemoryPack(payload: { messageId: number }) {
        let m = await messageApiForMemoryPack.get(payload.messageId);
        if (!m) throw Error();

        this.setMessage(m);
    }
}