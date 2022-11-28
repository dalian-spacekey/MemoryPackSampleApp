<template>
    <v-main>

        <v-responsive max-width="600" height="100vh" class="mx-auto">
            <v-card elevation="0" style="height:100%" v-if="message">

                <v-card-title>
                    <v-toolbar dense elevation="0">
                        <v-toolbar-title>
                            {{ message.title }}({{ type }})
                        </v-toolbar-title>
                        <v-spacer></v-spacer>
                        <v-btn icon>
                            <v-icon @click="close">mdi-close</v-icon>
                        </v-btn>
                    </v-toolbar>
                </v-card-title>
                <v-card-subtitle class="ms-4">
                    {{ message.publishDate | lastUpdatedFormat }}
                </v-card-subtitle>

                <v-card-text>
                    {{ message.contents }}
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
        lastUpdatedFormat(value) {
            if (!value) return '-';
            return utilities.dayjs(value).format('M/D HH:mm:ss');
        },
        captionDateFormat(value) {
            return utilities.dayjs(value).format('M/DD');
        }
    }
})
export default class MessageDetail extends Vue {
    loading: boolean = false;
    type: string = '';

    get message(): MessageForTypeScript | null {
        return messageStore.message;
    }

    async beforeMount() {
        this.load();
    }

    async load() {
        this.loading = true;
        const messageId = Number(this.$route.params.id);


        try {
            this.type = this.$route.query.type as string;

            switch (this.type) {
                case 'json':
                    await messageStore.getMessageJson({ messageId: messageId });
                    break;
                case 'memorypack':
                    await messageStore.getMessageMemoryPack({ messageId: messageId });
                    break;
                default:
                    this.type = 'json';
                    await messageStore.getMessageJson({ messageId: messageId });
                    break;
            }


        } catch {
            this.$router.replace('/Messages');
        }
        this.loading = false;
    }

    close() {
        this.$router.back();
    }
}
</script>