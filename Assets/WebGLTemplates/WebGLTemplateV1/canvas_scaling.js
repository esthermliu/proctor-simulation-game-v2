function resizeUnityCanvas() {
  const canvas = document.getElementById("unity-canvas");

  const targetAspect = 16 / 9;
  const windowAspect = window.innerWidth / window.innerHeight;

  let width, height;

  if (windowAspect > targetAspect) {
    // Window too wide → pillarbox
    height = window.innerHeight;
    width = height * targetAspect;
  } else {
    // Window too tall → letterbox
    width = window.innerWidth;
    height = width / targetAspect;
  }

  canvas.style.width = width + "px";
  canvas.style.height = height + "px";
}

window.addEventListener("resize", resizeUnityCanvas);
window.addEventListener("load", resizeUnityCanvas);