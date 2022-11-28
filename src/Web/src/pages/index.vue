<template>
    <v-main>

        <v-responsive max-width="600" height="100vh" class="mx-auto">
            <v-card elevation="0" style="height:100%">

                <v-card-title class="text-subtitle-1">
                    <h2>
                        MemoryPack Nuxt+TypeScript Sample
                    </h2>
                </v-card-title>

                <v-card-text>

                    <v-list-item two-line @click="goMessagesJson()">
                        <v-list-item-icon>
                            <v-icon>mdi-menu-right-outline</v-icon>
                        </v-list-item-icon>
                        <v-list-item-content>
                            <v-list-item-title>Messages</v-list-item-title>
                            <v-list-item-subtitle>Json</v-list-item-subtitle>
                        </v-list-item-content>
                    </v-list-item>

                    <v-list-item two-line @click="goMessagesMemoryPack()">
                        <v-list-item-icon>
                            <v-icon>mdi-menu-right-outline</v-icon>
                        </v-list-item-icon>
                        <v-list-item-content>
                            <v-list-item-title>Messages</v-list-item-title>
                            <v-list-item-subtitle>MemoryPack</v-list-item-subtitle>
                        </v-list-item-content>
                    </v-list-item>

                    <v-list-item two-line @click="goPersonsJson()">
                        <v-list-item-icon>
                            <v-icon>mdi-menu-right-outline</v-icon>
                        </v-list-item-icon>
                        <v-list-item-content>
                            <v-list-item-title>Persons</v-list-item-title>
                            <v-list-item-subtitle>Json</v-list-item-subtitle>
                        </v-list-item-content>
                    </v-list-item>

                    <v-list-item two-line @click="goPersonsMemoryPack()">
                        <v-list-item-icon>
                            <v-icon>mdi-menu-right-outline</v-icon>
                        </v-list-item-icon>
                        <v-list-item-content>
                            <v-list-item-title>Persons</v-list-item-title>
                            <v-list-item-subtitle>MemoryPack</v-list-item-subtitle>
                        </v-list-item-content>
                    </v-list-item>

                </v-card-text>
            </v-card>


        </v-responsive>

    </v-main>
</template>

<script lang="ts">
import { Component, Vue } from 'nuxt-property-decorator';

@Component
export default class IndexPage extends Vue {

    goMessagesJson() {
        this.$nuxt.$loading.start();
        this.$router.push({ path: '/Messages', query: {type: 'json' }});
        this.$nuxt.$loading.finish();
    }

    goMessagesMemoryPack() {
        this.$nuxt.$loading.start();
        this.$router.push({ path: '/Messages', query: {type: 'memorypack' }});
        this.$nuxt.$loading.finish();
    }

    goPersonsJson() {
        this.$nuxt.$loading.start();
        this.$router.push({ path: '/Persons', query: {type: 'json' }});
        this.$nuxt.$loading.finish();
    }

    goPersonsMemoryPack() {
        this.$nuxt.$loading.start();
        this.$router.push({ path: '/Persons', query: {type: 'memorypack' }});
        this.$nuxt.$loading.finish();
    }
}

</script>