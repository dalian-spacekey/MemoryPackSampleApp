<template>
    <v-main>

        <v-responsive max-width="600" height="100vh" class="mx-auto">
            <v-card elevation="0" class="mt-4" style="height:100%" v-if="message">

                <v-card-title>
                    <v-toolbar dense elevation="0">
                        <v-toolbar-title class="py-4">

                            <span v-if="!isEdit">{{ message.title }}({{ type }})</span>

                            <v-text-field v-if="isEdit" v-model="editedMessage.title" label="タイトル" outlined dense
                                autocomplete="off" />
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
                    <p v-if="!isEdit">
                        {{ message.contents }}
                    </p>

                    <v-textarea v-if="isEdit" v-model="editedMessage.contents" outlined />
                </v-card-text>

                <v-action>
                    <v-btn v-if="!isEdit" @click="edit">
                        Edit
                    </v-btn>
                    <v-btn v-if="isEdit" @click="isEdit = false">
                        Cancel
                    </v-btn>
                    <v-btn v-if="isEdit" color="primary" @click="save">
                        Save
                    </v-btn>
                </v-action>
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
    isEdit: boolean = false;
    editedMessage: MessageForTypeScript = new MessageForTypeScript();

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

    edit() {
        this.editedMessage.messageId = this.message!.messageId;
        this.editedMessage.title = this.message!.title;
        this.editedMessage.contents = this.message!.contents;
        this.isEdit = true;
    }

    async save() {
        this.loading = true;

        switch (this.type) {
            case 'json':
                await messageStore.updateJson(this.editedMessage!);
                break;
            case 'memorypack':
                await messageStore.updateMemoryPack(this.editedMessage!);
                break;
            default:
                this.type = 'json';
                await messageStore.updateJson(this.editedMessage!);
                break;
        }
        
        this.isEdit = false;
        this.$router.back();
        this.loading = false;
    }
}
</script>