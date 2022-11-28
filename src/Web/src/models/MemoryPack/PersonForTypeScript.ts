import { MemoryPackWriter } from "./MemoryPackWriter";
import { MemoryPackReader } from "./MemoryPackReader";

export class PersonForTypeScript {
    personId: string;
    name: string | null;
    nameKana: string | null;
    age: number;
    birthday: Date;
    gender: string | null;
    bloodType: string | null;
    email: string | null;
    phoneNumber: string | null;
    mobileNumber: string | null;
    postalCode: string | null;
    address: string | null;
    companyName: string | null;
    applicationDate: Date;
    withdrawalDate: Date | null;

    constructor() {
        this.personId = "00000000-0000-0000-0000-000000000000";
        this.name = null;
        this.nameKana = null;
        this.age = 0;
        this.birthday = new Date(0);
        this.gender = null;
        this.bloodType = null;
        this.email = null;
        this.phoneNumber = null;
        this.mobileNumber = null;
        this.postalCode = null;
        this.address = null;
        this.companyName = null;
        this.applicationDate = new Date(0);
        this.withdrawalDate = null;

    }

    static serialize(value: PersonForTypeScript | null): Uint8Array {
        const writer = MemoryPackWriter.getSharedInstance();
        this.serializeCore(writer, value);
        return writer.toArray();
    }

    static serializeCore(writer: MemoryPackWriter, value: PersonForTypeScript | null): void {
        if (value == null) {
            writer.writeNullObjectHeader();
            return;
        }

        writer.writeObjectHeader(15);
        writer.writeGuid(value.personId);
        writer.writeString(value.name);
        writer.writeString(value.nameKana);
        writer.writeInt32(value.age);
        writer.writeDate(value.birthday);
        writer.writeString(value.gender);
        writer.writeString(value.bloodType);
        writer.writeString(value.email);
        writer.writeString(value.phoneNumber);
        writer.writeString(value.mobileNumber);
        writer.writeString(value.postalCode);
        writer.writeString(value.address);
        writer.writeString(value.companyName);
        writer.writeDate(value.applicationDate);
        writer.writeNullableDate(value.withdrawalDate);

    }

    static serializeArray(value: (PersonForTypeScript | null)[] | null): Uint8Array {
        const writer = MemoryPackWriter.getSharedInstance();
        this.serializeArrayCore(writer, value);
        return writer.toArray();
    }

    static serializeArrayCore(writer: MemoryPackWriter, value: (PersonForTypeScript | null)[] | null): void {
        writer.writeArray(value, (writer, x) => PersonForTypeScript.serializeCore(writer, x));
    }

    static deserialize(buffer: ArrayBuffer): PersonForTypeScript | null {
        return this.deserializeCore(new MemoryPackReader(buffer));
    }

    static deserializeCore(reader: MemoryPackReader): PersonForTypeScript | null {
        const [ok, count] = reader.tryReadObjectHeader();
        if (!ok) {
            return null;
        }

        const value = new PersonForTypeScript();
        if (count == 15) {
            value.personId = reader.readGuid();
            value.name = reader.readString();
            value.nameKana = reader.readString();
            value.age = reader.readInt32();
            value.birthday = reader.readDate();
            value.gender = reader.readString();
            value.bloodType = reader.readString();
            value.email = reader.readString();
            value.phoneNumber = reader.readString();
            value.mobileNumber = reader.readString();
            value.postalCode = reader.readString();
            value.address = reader.readString();
            value.companyName = reader.readString();
            value.applicationDate = reader.readDate();
            value.withdrawalDate = reader.readNullableDate();

        }
        else if (count > 15) {
            throw new Error("Current object's property count is larger than type schema, can't deserialize about versioning.");
        }
        else {
            if (count == 0) return value;
            value.personId = reader.readGuid(); if (count == 1) return value;
            value.name = reader.readString(); if (count == 2) return value;
            value.nameKana = reader.readString(); if (count == 3) return value;
            value.age = reader.readInt32(); if (count == 4) return value;
            value.birthday = reader.readDate(); if (count == 5) return value;
            value.gender = reader.readString(); if (count == 6) return value;
            value.bloodType = reader.readString(); if (count == 7) return value;
            value.email = reader.readString(); if (count == 8) return value;
            value.phoneNumber = reader.readString(); if (count == 9) return value;
            value.mobileNumber = reader.readString(); if (count == 10) return value;
            value.postalCode = reader.readString(); if (count == 11) return value;
            value.address = reader.readString(); if (count == 12) return value;
            value.companyName = reader.readString(); if (count == 13) return value;
            value.applicationDate = reader.readDate(); if (count == 14) return value;
            value.withdrawalDate = reader.readNullableDate(); if (count == 15) return value;

        }
        return value;
    }

    static deserializeArray(buffer: ArrayBuffer): (PersonForTypeScript | null)[] | null {
        return this.deserializeArrayCore(new MemoryPackReader(buffer));
    }

    static deserializeArrayCore(reader: MemoryPackReader): (PersonForTypeScript | null)[] | null {
        return reader.readArray(reader => PersonForTypeScript.deserializeCore(reader));
    }
}
