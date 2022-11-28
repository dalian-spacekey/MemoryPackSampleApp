import { MemoryPackWriter } from "./MemoryPackWriter";
import { MemoryPackReader } from "./MemoryPackReader";

export class MessageForTypeScript {
    messageId: number;
    title: string | null;
    contents: string | null;
    publishDate: Date;

    constructor() {
        this.messageId = 0;
        this.title = null;
        this.contents = null;
        this.publishDate = new Date(0);

    }

    static serialize(value: MessageForTypeScript | null): Uint8Array {
        const writer = MemoryPackWriter.getSharedInstance();
        this.serializeCore(writer, value);
        return writer.toArray();
    }

    static serializeCore(writer: MemoryPackWriter, value: MessageForTypeScript | null): void {
        if (value == null) {
            writer.writeNullObjectHeader();
            return;
        }

        writer.writeObjectHeader(4);
        writer.writeInt32(value.messageId);
        writer.writeString(value.title);
        writer.writeString(value.contents);
        writer.writeDate(value.publishDate);

    }

    static serializeArray(value: (MessageForTypeScript | null)[] | null): Uint8Array {
        const writer = MemoryPackWriter.getSharedInstance();
        this.serializeArrayCore(writer, value);
        return writer.toArray();
    }

    static serializeArrayCore(writer: MemoryPackWriter, value: (MessageForTypeScript | null)[] | null): void {
        writer.writeArray(value, (writer, x) => MessageForTypeScript.serializeCore(writer, x));
    }

    static deserialize(buffer: ArrayBuffer): MessageForTypeScript | null {
        return this.deserializeCore(new MemoryPackReader(buffer));
    }

    static deserializeCore(reader: MemoryPackReader): MessageForTypeScript | null {
        const [ok, count] = reader.tryReadObjectHeader();
        if (!ok) {
            return null;
        }

        const value = new MessageForTypeScript();
        if (count == 4) {
            value.messageId = reader.readInt32();
            value.title = reader.readString();
            value.contents = reader.readString();
            value.publishDate = reader.readDate();

        }
        else if (count > 4) {
            throw new Error("Current object's property count is larger than type schema, can't deserialize about versioning.");
        }
        else {
            if (count == 0) return value;
            value.messageId = reader.readInt32(); if (count == 1) return value;
            value.title = reader.readString(); if (count == 2) return value;
            value.contents = reader.readString(); if (count == 3) return value;
            value.publishDate = reader.readDate(); if (count == 4) return value;

        }
        return value;
    }

    static deserializeArray(buffer: ArrayBuffer): (MessageForTypeScript | null)[] | null {
        return this.deserializeArrayCore(new MemoryPackReader(buffer));
    }

    static deserializeArrayCore(reader: MemoryPackReader): (MessageForTypeScript | null)[] | null {
        return reader.readArray(reader => MessageForTypeScript.deserializeCore(reader));
    }
}
