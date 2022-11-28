# MemoryPack Sample App

まずは、ASP.NET CoreのサーバーとNuxtでのやりとりはこうなるんじゃないかというサンプル。

## 構成

* Server: ASP.NET Core + Entity Framework Core(SQLite)
* Web: Nuxt 2(TypeScript)


## Server

Visual Studio 2022 Version 17.4以降でデバッグ実行とか、.NET 7環境で `dotnet run` で動かすと、`https://localhost:7287/swagger/index.html` で実装されているAPIメソッドが確認できます。

## Web

`src/Web` ディレクトリで、`npm install` とか `yarn install` でセットアップして、`npm run dev` とか `yarn dev` で実行すると、`http://localhost:3000/`でWebアプリが起動します。

`Message` と `Person`の一覧→詳細ページが実装されていて、それぞれServerからJsonとMemoryPackでデータを受け取れるようになっています。
