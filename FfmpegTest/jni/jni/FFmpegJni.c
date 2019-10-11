#include "logjni.h"
#include "FFmpegJni.h"

#include <stdlib.h>
#include <stdbool.h>

int main(int argc, char **argv);

JNIEXPORT jint JNICALL Java_org_mqstack_ffmpegjni_FFmpegJni_run(JNIEnv *env, jint argc, char **args) {
   /* int i = 0;
    char **argv = NULL;
    jstring *strr = NULL;

LOGD("argc:%d",argc);
LOGD("args:%s",args);
    if (args != NULL) {
        argv = (char **) malloc(sizeof(char *) * argc);
        strr = (jstring *) malloc(sizeof(jstring) * argc);
	LOGD("NOT NULL");
        for (i = 0; i < argc; ++i) {
LOGD("IN FOR");
            strr[i] = (jstring)(*env)->GetObjectArrayElement(env, args, i);
LOGD("strr:%s",strr[i]);
            argv[i] = (char *)(*env)->GetStringUTFChars(env, strr, 0);
            LOGD("ffmpeg args: %s", argv[i]);
        }
    }
*/
LOGD("argc:%d",argc);
int i;
for(i=0;i<argc;i++){
LOGD("args:%s",args[i]);
}
    LOGD("Run ffmpeg");
    int result = main(argc, args);
    LOGD("ffmpeg result %d", result);

 /*   for (i = 0; i < argc; ++i) {
        (*env)->ReleaseStringUTFChars(env, strr[i], argv[i]);
    }
    free(argv);
    free(strr);
*/
    return result;
}
JNIEXPORT void JNICALL Java_org_mqstack_ffmpegjni_FFmpegJni_test(JNIEnv *env,  jint argc, jstring str) {
   // const char *Str=(*env)->GetStringUTFChars(env,str,0);
LOGD("%d",argc);
LOGD("%s",str);
//LOGD("%s",Str);
 //(*env) ->ReleaseStringUTFChars(env,str,Str); 
}
