import { Store } from 'vuex';
import { getModule } from 'vuex-module-decorators';
import MessageStore from './MessageStore';
import PersonStore from './PersonStore';

let messageStore: MessageStore;
let personStore: PersonStore;

const initializer = (store: Store<any>) => {
    messageStore = getModule(MessageStore, store);
    personStore = getModule(PersonStore, store);
}

export const plugins = [initializer];

export {
    initializer,
    messageStore,
    personStore
};