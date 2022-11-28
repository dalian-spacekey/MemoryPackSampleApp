<template>
    <v-main>

        <v-responsive max-width="600" height="100vh" class="mx-auto">
            <v-card elevation="0" style="height:100%" v-if="person">

                <v-card-title>
                    <v-toolbar dense elevation="0">
                        <v-toolbar-title>
                            {{ person.name }}({{ type }})
                        </v-toolbar-title>
                        <v-spacer></v-spacer>
                        <v-btn icon>
                            <v-icon @click="close">mdi-close</v-icon>
                        </v-btn>
                    </v-toolbar>
                </v-card-title>
                <v-card-subtitle class="ms-4">
                    {{ person.personId }}
                </v-card-subtitle>

                <v-card-text>
                    <ul>
                        <li>Age: {{ person.age }}</li>
                        <li>Birthday: {{ person.age }}</li>
                        <li>Gender: {{ person.gender }}</li>
                        <li>BloodType: {{ person.bloodType }}</li>
                        <li>Email: {{ person.email }}</li>
                        <li>PhoneNumber: {{ person.phoneNumber }}</li>
                        <li>MobileNumber: {{ person.mobileNumber }}</li>
                        <li>PostalCode: {{ person.postalCode }}</li>
                        <li>CompanyName: {{ person.companyName }}</li>
                        <li>ApplicationDate: {{ person.applicationDate }}</li>
                        <li>WithdrawalDate: {{ person.withdrawalDate }}</li>
                    </ul>
                    
                </v-card-text>
            </v-card>
        </v-responsive>

    </v-main>
</template>

<script lang="ts">
import { Component, Vue } from 'nuxt-property-decorator';
import { personStore } from '~/store';
import { PersonForTypeScript } from '~/models';
import * as utilities from '~/utilities';

@Component({
    filters: {
        lastUpdatedFormat(value) {
            if (!value) return '-';
            return utilities.dayjs(value).format('M/D HH:mm:ss');
        },
        captionDateFormat(value) {
            return utilities.dayjs(value).format('M/DD');
        }
    }
})
export default class PersonDetail extends Vue {
    loading: boolean = false;
    type: string = '';

    get person(): PersonForTypeScript | null {
        return personStore.person;
    }

    async beforeMount() {
        this.load();
    }

    async load() {
        this.loading = true;
        const personId = Number(this.$route.params.id);


        try {
            this.type = this.$route.query.type as string;

            switch (this.type) {
                case 'json':
                    await personStore.getPersonJson({ personId: personId });
                    break;
                case 'memorypack':
                    await personStore.getPersonMemoryPack({ personId: personId });
                    break;
                default:
                    this.type = 'json';
                    await personStore.getPersonJson({ personId: personId });
                    break;
            }


        } catch {
            this.$router.replace('/Persons');
        }
        this.loading = false;
    }

    close() {
        this.$router.back();
    }
}
</script>