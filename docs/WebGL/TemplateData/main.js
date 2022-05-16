var canvas = document.querySelector("#unity-canvas");

var config = {
    dataUrl: "Build/WebGL.data",
    frameworkUrl: "Build/WebGL.framework.js",
    codeUrl: "Build/WebGL.wasm",
    streamingAssetsUrl: "StreamingAssets",
    companyName: "Andrew Allbright",
    productName: "Destructible Zombie Microprototype",
    productVersion: "0.0.19",
    devicePixelRatio: 1,
}

createUnityInstance(canvas, config);
