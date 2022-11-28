import { Module, VuexModule, Mutation, Action } from 'vuex-module-decorators';
import { personApiForJson, personApiForMemoryPack } from '~/api';
import { PersonForTypeScript } from '~/models';
import * as utilities from '~/utilities';

@Module({
    name: 'PersonStore',
    stateFactory: true,
    namespaced: true
})
export default class PersonStore extends VuexModule {
    persons: PersonForTypeScript[] = [];
    person: PersonForTypeScript | null = null;
    
    @Mutation
    setPersons(persons: PersonForTypeScript[]) {
        this.persons = persons;
    }

    @Mutation
    setPerson(person: PersonForTypeScript) {
        this.person = person;
    }

    @Action
    async getPersonsJson() {
        const data = await personApiForJson.getAll();

        if (data) {
            this.setPersons(data);
        }
    }

    @Action
    async getPersonJson(payload: { personId: number }) {

        let m = await personApiForJson.get(payload.personId);
        if (!m) throw Error();

        this.setPerson(m);
    }

    @Action
    async getPersonsMemoryPack() {
        const data = await personApiForMemoryPack.getAll();

        if (data) {
            this.setPersons(data);
        }
    }

    @Action
    async getPersonMemoryPack(payload: { personId: number }) {
        let m = await personApiForMemoryPack.get(payload.personId);
        if (!m) throw Error();

        this.setPerson(m);
    }
}