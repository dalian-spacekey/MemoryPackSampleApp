<template>
    <v-main>

        <v-container max-width="600" height="100vh" class="mx-auto">

            <v-card elevation="0" style="height:100%">

                <v-card-title class="text-subtitle-1">
                    <v-icon>mdi-menu-right-outline</v-icon>
                        Persons({{ type }})
                    <v-spacer></v-spacer>
                    <v-btn icon color="primary" @click="reload">
                        <v-icon>mdi-reload</v-icon>
                    </v-btn>
                </v-card-title>
            </v-card>

            <v-data-table :headers="headers" :items="persons" class="elevation-1 mt-2" disable-sort :loading="listLoading"
                no-data-text="no person" @click:row="openDetail">
                <template v-slot:top>
                    <v-expansion-panels flat v-if="false">
                        <v-expansion-panel>
                            <v-expansion-panel-header v-slot="{ open }">
                                <v-row no-gutters>
                                    <v-col class="text--secondary">
                                        <v-fade-transition leave-absolute>
                                            <v-row v-if="!open" no-gutters style="width: 100%">
                                                <v-col />
                                            </v-row>
                                        </v-fade-transition>
                                    </v-col>
                                </v-row>
                            </v-expansion-panel-header>
                            <v-expansion-panel-content>
                                <v-row no-gutters>
                                    <v-col />
                                </v-row>
                            </v-expansion-panel-content>
                            <v-divider />
                        </v-expansion-panel>
                    </v-expansion-panels>
                </template>
            </v-data-table>

        </v-container>

    </v-main>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'nuxt-property-decorator';
import { personStore } from '~/store';
import { PersonForTypeScript } from '~/models';
import * as utilities from '~/utilities';

@Component({
    filters: {
        dateTimeFormat(value) {
            if (!value) return '-';
            return utilities.dayjs(value).format('YYYY/M/D HH:mm:ss');
        },
        textOverflow(value) {
            return value.length < 100 ? value : value.substring(0, 100) + '...';
        }
    }
})
export default class PersonIndexPage extends Vue {
    listLoading: boolean = false;
    type: string = '';

    headers = [
        {
            text: 'ID',
            value: 'personId'
        },
        {
            text: 'Name',
            value: 'name'
        },
        {
            text: 'Kana',
            value: 'nameKana'
        },
        {
            text: 'Email',
            value: 'email'
        }
    ];

    get persons() {
        return personStore.persons;
    }

    async beforeMount() {
        this.load();
    }

    async load() {
        this.listLoading = true;

        this.type = this.$route.query.type as string;

        switch (this.type) {
            case 'json':
                await personStore.getPersonsJson();
                break;
            case 'memorypack':
                await await personStore.getPersonsMemoryPack();
                break;
            default:
                this.type = 'json';
                await personStore.getPersonsJson();
                break;
        }

        this.listLoading = false;
    }

    reload() {
        this.load();
    }

    openDetail(item: PersonForTypeScript) {
        this.$nuxt.$loading.start();
        this.$router.push({ path: `/Persons/${item.personId}`, query: { type: this.type } });
        this.$nuxt.$loading.finish();
    }
}

</script>

<style>
.text-overflow {
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
}
</style>