export interface Message {
    id?: number; // int32
    content?: string | null;
    senderId?: number | null; // int32
    sendDate?: string | null; // date-time
}

export interface User {
    id?: number; // int32
    alias?: string | null;
    password?: string | null;
}
