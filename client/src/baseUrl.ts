const isProdiction = import.meta.env.PROD;
import {LibraryClient} from "./generated-client.ts";

const isProdiction = import.meta.env.PROD;

const prod = "https://server-empty-darkness-8887.fly.dev";
const dev = "http://localhost:5173/"

export const finalUrl = isProdiction ? prod : dev;


export const libraryClient = new LibraryClient(finalUrl)