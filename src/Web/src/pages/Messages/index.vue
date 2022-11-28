<template>
    <v-main>

        <v-responsive max-width="600" height="100vh" class="mx-auto">


            <v-card elevation="0" style="height:100%">

                <v-card-title class="text-subtitle-1">
                    <v-icon>mdi-menu-right-outline</v-icon>
                    Messages({{ type }})
                    <v-spacer></v-spacer>
                    <v-btn icon color="primary" @click="reload">
                        <v-icon>mdi-reload</v-icon>
                    </v-btn>
                </v-card-title>

                <v-card-text style="height:90%">
                    <v-virtual-scroll :items="messages" item-height="200" style="height:100%">
                        <template v-slot:default="{ item }">
                            <v-hover v-slot:default="{ hover }">
                                <v-card :elevation="hover ? 6 : 2" style="cursor: pointer" class="mx-2" @click="openDetail(item)">
                                    <v-card-title>
                                        {{ item.title }}
                                    </v-card-title>
                                    <v-card-subtitle>
                                        {{ item.publishDate | dateTimeFormat }}
                                    </v-card-subtitle>
                                    <v-card-text>
                                        {{ item.contents | textOverflow }}
                                    </v-card-text>
                                </v-card>
                            </v-hover>
                        </template>
                    </v-virtual-scroll>
                </v-card-text>
            </v-card>

        </v-responsive>

    </v-main>
</template>

<script lang="ts">
import { Component, Vue } from 'nuxt-property-decorator';
import { messageStore } from '~/store';
import { MessageForTypeScript } from '~/models';
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
export default class MessageIndexPage extends Vue {
    isLoading: boolean = false;
    type: string = '';

    get messages() {
        return messageStore.messages;
    }

    async beforeMount() {
        this.load();
    }
    
    async load() {
        this.isLoading = true;

        this.type = this.$route.query.type as string;

        switch (this.type) {
            case 'json':
                await messageStore.getMessagesJson();
                break;
            case 'memorypack':
                await await messageStore.getMessagesMemoryPack();
                break;
            default:
                this.type = 'json';
                await messageStore.getMessagesJson();
                break;
        }

        this.isLoading = false;
    }

    reload() {
        this.load();
    }

    openDetail(item: MessageForTypeScript) {
        this.$nuxt.$loading.start();
        this.$router.push({ path: `/Messages/${item.messageId}`, query: { type: this.type }});
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