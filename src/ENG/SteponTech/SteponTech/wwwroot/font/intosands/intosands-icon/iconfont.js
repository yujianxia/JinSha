(function(window){var svgSprite="<svg>"+""+'<symbol id="icon-bofang-copy" viewBox="0 0 1024 1024">'+""+'<path d="M511.999 1015.391c-278.032 0-503.41-225.395-503.41-503.389 0-278.020 225.38-503.389 503.41-503.389 278.006 0 503.41 225.37 503.41 503.389 0 277.995-225.405 503.389-503.41 503.389zM511.999 123.743c-214.416 0-388.251 173.826-388.251 388.235 0 214.432 173.834 388.236 388.251 388.236 214.442 0 388.251-173.802 388.251-388.209 0-214.432-173.81-388.258-388.251-388.258z"  ></path>'+""+'<path d="M369.087 674.35c0 5.825 2.951 11.527 8.259 14.82 5.333 3.27 11.725 3.369 16.961 0.739l324.733-162.348c5.703-2.853 9.611-8.75 9.611-15.559 0-6.833-3.907-12.732-9.611-15.584l-324.734-162.373c-5.235-2.605-11.626-2.507-16.961 0.761-5.31 3.294-8.259 8.996-8.259 14.82v324.722h0.001z"  ></path>'+""+"</symbol>"+""+'<symbol id="icon-icon07" viewBox="0 0 1024 1024">'+""+'<path d="M132.678 350.212c0-6.626 2.249-13.285 6.85-18.75 10.363-12.304 28.739-13.876 41.042-3.514l366.304 308.539 351.945-294.3c12.34-10.32 30.709-8.683 41.029 3.659 10.319 12.34 8.681 30.709-3.66 41.028l-370.698 309.981c-10.845 9.069-26.637 9.041-37.449-0.066l-385.001-324.286c-6.841-5.762-10.362-13.999-10.363-22.292z"  ></path>'+""+"</symbol>"+""+"</svg>";var script=function(){var scripts=document.getElementsByTagName("script");return scripts[scripts.length-1]}();var shouldInjectCss=script.getAttribute("data-injectcss");var ready=function(fn){if(document.addEventListener){if(~["complete","loaded","interactive"].indexOf(document.readyState)){setTimeout(fn,0)}else{var loadFn=function(){document.removeEventListener("DOMContentLoaded",loadFn,false);fn()};document.addEventListener("DOMContentLoaded",loadFn,false)}}else if(document.attachEvent){IEContentLoaded(window,fn)}function IEContentLoaded(w,fn){var d=w.document,done=false,init=function(){if(!done){done=true;fn()}};var polling=function(){try{d.documentElement.doScroll("left")}catch(e){setTimeout(polling,50);return}init()};polling();d.onreadystatechange=function(){if(d.readyState=="complete"){d.onreadystatechange=null;init()}}}};var before=function(el,target){target.parentNode.insertBefore(el,target)};var prepend=function(el,target){if(target.firstChild){before(el,target.firstChild)}else{target.appendChild(el)}};function appendSvg(){var div,svg;div=document.createElement("div");div.innerHTML=svgSprite;svgSprite=null;svg=div.getElementsByTagName("svg")[0];if(svg){svg.setAttribute("aria-hidden","true");svg.style.position="absolute";svg.style.width=0;svg.style.height=0;svg.style.overflow="hidden";prepend(svg,document.body)}}if(shouldInjectCss&&!window.__iconfont__svg__cssinject__){window.__iconfont__svg__cssinject__=true;try{document.write("<style>.svgfont {display: inline-block;width: 1em;height: 1em;fill: currentColor;vertical-align: -0.1em;font-size:16px;}</style>")}catch(e){console&&//console.log(e)}}ready(appendSvg)})(window)
