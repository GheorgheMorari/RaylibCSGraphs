using System;
using System.Runtime.InteropServices;
using RaylibSharp.Raylib.Types;

namespace RaylibSharp //kek
{
    namespace Raylib
    {
        public enum ConfigFlag : byte
        {
            FlagShowLogo = 1,
            FlagFullscreenMode = 2,
            FlagWindowResizable = 4,
            FlagWindowUndecorated = 8,
            FlagWindowTransparent = 16,
            FlagMsaa_4XHint = 32,
            FlagVsyncHint = 64
        }

        public enum TraceLogFlag : byte
        {
            LogInfo = 1,
            LogWarning = 2,
            LogError = 4,
            LogDebug = 8,
            LogOther = 16
        }

        public enum CameraMode
        {
            CameraCustom = 0,
            CameraFree,
            CameraOribital,
            CameraFirstPerson,
            CameraThirdPerson
        }

        public enum GestureType : uint
        {
            GestureNone = 0,
            GestureTap = 1,
            GestureDoubletap = 2,
            GestureHold = 4,
            GestureDrag = 8,
            GestureSwipeRight = 16,
            GestureSwipeLeft = 32,
            GestureSwipeUp = 64,
            GestureSwipeDown = 128,
            GesturePinchIn = 256,
            GesturePinchOut = 512
        }

        public enum CameraType
        {
            CameraPerspective = 0,
            CameraOrthographic
        }

        public enum FilterMode
        {
            FilterPoint = 0,
            FilterBilinear,
            FilterTrilinear,
            FilterAnisotropic4X,
            FilterAnisotropic8X,
            FilterAnisotropic16X
        }

        public enum WrapMode
        {
            WrapRepeat = 0,
            WrapClamp,
            WrapMirror
        }

        public enum BlendMode
        {
            BlendAlpha = 0,
            BlendAdditive,
            BlendMultiply
        }

        public enum VrDeviceType
        {
            HmdDefaultDevice = 0,
            HmdOculusRiftDk2,
            HmdOculusRiftDk1,
            HmdOculusGo,
            HmdValveHtcVive,
            HmdSonyPsVr
        }

        public enum KeyboardKey
        {
            // keyboard keys
            KeySpace = 32,

            KeyEscape = 256,
            KeyEnter = 257,
            KeyTab = 258,
            KeyBackspace = 259,
            KeyInsert = 260,
            KeyDelete = 261,
            KeyRight = 262,
            KeyLeft = 263,
            KeyDown = 264,
            KeyUp = 265,
            KeyPageUp = 266,
            KeyPageDown = 267,
            KeyHome = 268,
            KeyEnd = 269,
            KeyCapsLock = 280,
            KeyScrollLock = 281,
            KeyNumLock = 282,
            KeyPrintScreen = 283,
            KeyPause = 284,
            KeyF1 = 290,
            KeyF2 = 291,
            KeyF3 = 292,
            KeyF4 = 293,
            KeyF5 = 294,
            KeyF6 = 295,
            KeyF7 = 296,
            KeyF8 = 297,
            KeyF9 = 298,
            KeyF10 = 299,
            KeyF11 = 300,
            KeyF12 = 301,
            KeyLeftShift = 340,
            KeyLeftControl = 341,
            KeyLeftAlt = 342,
            KeyRightShift = 344,
            KeyRightControl = 345,
            KeyRightAlt = 346,
            KeyGrave = 96,
            KeySlash = 47,
            KeyBackslash = 92,
            KeyZero = 48,
            KeyOne = 49,
            KeyTwo = 50,
            KeyThree = 51,
            KeyFour = 52,
            KeyFive = 53,
            KeySix = 54,
            KeySeven = 55,
            KeyEight = 56,
            KeyNine = 57,
            KeyA = 65,
            KeyB = 66,
            KeyC = 67,
            KeyD = 68,
            KeyE = 69,
            KeyF = 70,
            KeyG = 71,
            KeyH = 72,
            KeyI = 73,
            KeyJ = 74,
            KeyK = 75,
            KeyL = 76,
            KeyM = 77,
            KeyN = 78,
            KeyO = 79,
            KeyP = 80,
            KeyQ = 81,
            KeyR = 82,
            KeyS = 83,
            KeyT = 84,
            KeyU = 85,
            KeyV = 86,
            KeyW = 87,
            KeyX = 88,
            KeyY = 89,
            KeyZ = 90,

            // android keys
            KeyBack = 4,

            KeyMenu = 82,
            KeyVolumeUp = 24,
            KeyVolumeDown = 25
        }

        public enum MouseButton
        {
            MouseLeftButton = 0,
            MouseRightButton = 1,
            MouseMiddleButton = 2
        }

        namespace Types
        {
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Color
            {
                public byte r;
                public byte g;
                public byte b;
                public byte a;

                public static readonly Color WHITE = new Color(255, 255, 255);
                public static readonly Color BLACK = new Color(0, 0, 0);

                public Color(byte r, byte g, byte b, byte a = 255)
                {
                    this.r = r;
                    this.g = g;
                    this.b = b;
                    this.a = a;
                }
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Image
            {
                public unsafe void* data;
                public int width;
                public int height;
                public int mipmaps;
                public int format;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Texture2D
            {
                public uint id;
                public int width;
                public int height;
                public int mipmaps;
                public int format;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Vector4
            {
                public float x;
                public float y;
                public float z;
                public float w;

                public Vector4(float x, float y, float z, float w)
                {
                    this.x = x;
                    this.y = y;
                    this.z = z;
                    this.w = w;
                }
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Vector3
            {
                public float x;
                public float y;
                public float z;

                public Vector3(float x, float y, float z)
                {
                    this.x = x;
                    this.y = y;
                    this.z = z;
                }
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Vector2
            {
                public float x;
                public float y;

                public Vector2(float x, float y)
                {
                    this.x = x;
                    this.y = y;
                }
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Rectangle
            {
                public float x;
                public float y;
                public float width;
                public float height;

                public Rectangle(float x, float y, float width, float height)
                {
                    this.x = x;
                    this.y = y;
                    this.width = width;
                    this.height = height;
                }
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Camera3D
            {
                public Vector3 position;
                public Vector3 target;
                public Vector3 up;
                public float fovy;
                public CameraType type;

                public Camera3D(Vector3 position, Vector3 target, Vector3 up, float fovy = 90,
                    CameraType type = CameraType.CameraPerspective)
                {
                    this.position = position;
                    this.target = target;
                    this.up = up;
                    this.fovy = fovy;
                    this.type = type;
                }
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Camera2D
            {
                public Vector2 offset;
                public Vector2 target;
                public float rotation;
                public float zoom;

                public Camera2D(Vector2 offset, Vector2 target, float rotation = 0, float zoom = 1)
                {
                    this.offset = offset;
                    this.target = target;
                    this.rotation = rotation;
                    this.zoom = zoom;
                }
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct RenderTexture2D
            {
                public uint id;
                public Texture2D texture;
                public Texture2D depth;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Ray
            {
                public Vector3 position;
                public Vector3 direction;

                public Ray(Vector3 position, Vector3 direction)
                {
                    this.position = position;
                    this.direction = direction;
                }
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct RayHitInfo
            {
                public bool hit;
                public float distance;
                public Vector3 position;
                public Vector3 normal;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Matrix
            {
                public float m0, m4, m8, m12;
                public float m1, m5, m9, m13;
                public float m2, m6, m10, m14;
                public float m3, m7, m11, m15;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct CharInfo
            {
                public int value;
                public Rectangle rec;
                public int offsetX;
                public int offsetY;
                public int advanceX;
                public unsafe void* data;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Font
            {
                public Texture2D texture;
                public int baseSize;
                public int charsCount;
                public unsafe CharInfo* chars;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public unsafe struct Shader
            {
                public uint id;
                public fixed int locs[32];
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct MaterialMap
            {
                public Texture2D texture;
                public Color color;
                public float value;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Material
            {
                public Shader shader;
                public unsafe fixed byte maps[336];
                public unsafe float* args;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Mesh
            {
                public int vertexCount;
                public int triangleCount;

                public unsafe float* vertices;
                public unsafe float* texcoords;
                public unsafe float* texcoords2;
                public unsafe float* normals;
                public unsafe float* tangents;
                public unsafe byte* colors;
                public unsafe ushort* indices;

                public uint vaoId;
                public unsafe fixed uint vboId[7];
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Model
            {
                public Mesh mesh;
                public Matrix transform;
                public Material material;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct BoundingBox
            {
                public Vector3 min;
                public Vector3 max;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Wave
            {
                public uint sampleCount;
                public uint sampleRate;
                public uint sampleSize;
                public uint channels;
                public unsafe void* data;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct Sound
            {
                public unsafe void* audioBuffer;
                public uint source;
                public uint buffer;
                public int format;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct AudioStream
            {
                public uint sampleRate;
                public uint sampleSize;
                public uint channels;
                public unsafe void* audioBuffer;
                public int format;
                public uint source;
                public unsafe fixed uint buffers[2];
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct VrDeviceInfo
            {
                public int hResolution;
                public int vResolution;
                public float hScreenSize;
                public float vScreenSize;
                public float vScreenCenter;
                public float eyeToScreenDistance;
                public float lensSeparationDistance;
                public float interpupillaryDistance;
                public unsafe fixed float lensDistortionValues[4];
                public unsafe fixed float chromaAbCorrection[4];
            }
        }

        public static class Raylib
        {
            // device
            [DllImport(@"raylib.dll")]
            public static extern void InitAudioDevice();

            [DllImport(@"raylib.dll")]
            public static extern void CloseAudioDevice();

            [DllImport(@"raylib.dll")]
            public static extern bool IsAudioDeviceReady();

            [DllImport(@"raylib.dll")]
            public static extern void SetMasterVolume(float volume);

            // load sound
            [DllImport(@"raylib.dll")]
            public static extern Wave LoadWave(string fileName);

            [DllImport(@"raylib.dll")]
            public static extern Wave LoadWaveEx(float[] data, int sampleCount, int sampleRate, int sampleSize,
                int channels);

            [DllImport(@"raylib.dll")]
            public static extern Sound LoadSound(string fileName);

            [DllImport(@"raylib.dll")]
            public static extern Sound LoadSoundFromwave(Wave wave);

            [DllImport(@"raylib.dll")]
            public static extern void UpdateSound(Sound sound, byte[] data, int numSampleS);

            [DllImport(@"raylib.dll")]
            public static extern void UnloadWave(Wave wave);

            [DllImport(@"raylib.dll")]
            public static extern void UnloadSound(Sound sound);

            // manage sound
            [DllImport(@"raylib.dll")]
            public static extern void PlaySound(Sound sound);

            [DllImport(@"raylib.dll")]
            public static extern void PauseSound(Sound sound);

            [DllImport(@"raylib.dll")]
            public static extern void ResumeSound(Sound sound);

            [DllImport(@"raylib.dll")]
            public static extern void StopSound(Sound sound);

            [DllImport(@"raylib.dll")]
            public static extern bool IsSoundPlaying(Sound sound);

            [DllImport(@"raylib.dll")]
            public static extern void SetSoundVolume(Sound sound, float volume);

            [DllImport(@"raylib.dll")]
            public static extern void SetSoundPitch(Sound sound, float pitch);

            [DllImport(@"raylib.dll")]
            public static extern void WaveFormat(out Wave wave, int sampleRate, int sampleSize, int channels);

            [DllImport(@"raylib.dll")]
            public static extern Wave WaveCopy(Wave wave);

            [DllImport(@"raylib.dll")]
            public static extern void WaveCrop(out Wave wave, int initSample, int finalSample);

            [DllImport(@"raylib.dll")]
            public static extern float[] GetWaveData(Wave wave);

            // music
            [DllImport(@"raylib.dll")]
            public static extern IntPtr LoadMusicStream(string fileName);

            [DllImport(@"raylib.dll")]
            public static extern void UnloadMusicStream(IntPtr music);

            [DllImport(@"raylib.dll")]
            public static extern void PlayMusicStream(IntPtr music);

            [DllImport(@"raylib.dll")]
            public static extern void UpdateMusicStream(IntPtr music);

            [DllImport(@"raylib.dll")]
            public static extern void StopMusicStream(IntPtr music);

            [DllImport(@"raylib.dll")]
            public static extern void PauseMusicStream(IntPtr music);

            [DllImport(@"raylib.dll")]
            public static extern void ResumeMusicStream(IntPtr music);

            [DllImport(@"raylib.dll")]
            public static extern bool IsMusicPlaying(IntPtr music);

            [DllImport(@"raylib.dll")]
            public static extern void SetMusicVolume(IntPtr music, float volume);

            [DllImport(@"raylib.dll")]
            public static extern void SetMusicPitch(IntPtr music, float pitch);

            [DllImport(@"raylib.dll")]
            public static extern void SetMusicLoopCount(IntPtr music, float count);

            [DllImport(@"raylib.dll")]
            public static extern float GetMusicTimeLength(IntPtr music);

            [DllImport(@"raylib.dll")]
            public static extern float GetMusicTimePlayed(IntPtr music);

            // audiostream
            [DllImport(@"raylib.dll")]
            public static extern AudioStream InitAudioStream(uint sampleRate, uint sampleSize, uint channels);

            [DllImport(@"raylib.dll")]
            public static extern void UpdateAudioStream(AudioStream stream, byte[] data, int numSamples);

            [DllImport(@"raylib.dll")]
            public static extern void CloseAudioStream(AudioStream stream);

            [DllImport(@"raylib.dll")]
            public static extern bool IsAudioBufferProcessed(AudioStream stream);

            [DllImport(@"raylib.dll")]
            public static extern void PlayAudioStream(AudioStream stream);

            [DllImport(@"raylib.dll")]
            public static extern void PauseAudioStream(AudioStream stream);

            [DllImport(@"raylib.dll")]
            public static extern void ResumeAudioStream(AudioStream stream);

            [DllImport(@"raylib.dll")]
            public static extern void StopAudioStream(AudioStream stream);

            // loading
            [DllImport(@"raylib.dll")]
            public static extern string LoadText(string fileName);

            [DllImport(@"raylib.dll")]
            public static extern Shader LoadShader(string vsFileName, string fsFileName);

            [DllImport(@"raylib.dll")]
            public static extern Shader LoadShaderCode(string vsCode, string fsCode);

            [DllImport(@"raylib.dll")]
            public static extern void UnloadShader(Shader shader);

            [DllImport(@"raylib.dll")]
            public static extern Shader GetShaderDefault();

            [DllImport(@"raylib.dll")]
            public static extern Texture2D GetTextureDefault();

            // uniforms
            [DllImport(@"raylib.dll")]
            public static extern int GetShaderLocation(Shader shader, string uniformName);

            [DllImport(@"raylib.dll")]
            public static extern void SetShaderValue(Shader shader, int uniformLoc, float[] value, int size);

            [DllImport(@"raylib.dll")]
            public static extern void SetShaderValuei(Shader shader, int uniformLoc, int[] value, int size);

            [DllImport(@"raylib.dll")]
            public static extern void SetShaderValueMatrix(Shader shader, int uniformLoc, Matrix mat);

            [DllImport(@"raylib.dll")]
            public static extern void SetMatrixProjection(Matrix proj);

            [DllImport(@"raylib.dll")]
            public static extern void SetMatrixModelview(Matrix view);

            [DllImport(@"raylib.dll")]
            public static extern Matrix GetMatrixModelview();

            // drawing
            [DllImport(@"raylib.dll")]
            public static extern void BeginShaderMode(Shader shader);

            [DllImport(@"raylib.dll")]
            public static extern void EndShaderMode();

            [DllImport(@"raylib.dll")]
            public static extern void BeginBlendMode(BlendMode mode);

            [DllImport(@"raylib.dll")]
            public static extern void EndBlendMode();

            // vr stuff
            [DllImport(@"raylib.dll")]
            public static extern VrDeviceInfo GetVrDeviceInfo(VrDeviceType vrDeviceType);

            [DllImport(@"raylib.dll")]
            public static extern void InitVrSimulator(VrDeviceInfo info);

            [DllImport(@"raylib.dll")]
            public static extern void CloseVrSimulator();

            [DllImport(@"raylib.dll")]
            public static extern bool IsVrSimulatorReady();

            [DllImport(@"raylib.dll")]
            public static extern void SetVrDistortionShader(Shader shader);

            [DllImport(@"raylib.dll")]
            public static extern void UpdateVrTracking(out Camera3D camera);

            [DllImport(@"raylib.dll")]
            public static extern void ToggleVrMode();

            [DllImport(@"raylib.dll")]
            public static extern void BeginVrDrawing();

            [DllImport(@"raylib.dll")]
            public static extern void EndVrDrawing();

            // 3d primitives
            [DllImport(@"raylib.dll")]
            public static extern void DrawLine3D(Vector3 startPos, Vector3 endPos, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawCircle3D(Vector3 center, float radius, Vector3 rotationAxis,
                float rotationAngle, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawCube(Vector3 position, float width, float height, float length, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawCubeV(Vector3 position, Vector3 size, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawCubeWires(Vector3 position, float width, float height, float length,
                Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawSphere(Vector3 centerPos, float radius, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawSphereEx(Vector3 centerPos, float radius, int rings, int slices, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawSphereWires(Vector3 centerPos, float radus, int rings, int slices,
                Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawCylinder(Vector3 centerPos, float radiusTop, float radiusBottom, float height,
                int slices, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawCylinderWires(Vector3 centerPos, float radiusTop, float radiusBottom,
                float height, int slices, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawPlane(Vector3 centerPos, Vector2 size, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawRay(Ray rat, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawGrid(int slices, float spacing);

            [DllImport(@"raylib.dll")]
            public static extern void DrawGizmo(Vector3 position);

            // model loading
            [DllImport(@"raylib.dll")]
            public static extern Model LoadModel(string fileName);

            [DllImport(@"raylib.dll")]
            public static extern Model LoadModelFromMesh(Mesh mesh);

            [DllImport(@"raylib.dll")]
            public static extern Model UnloadModel(Model model);

            [DllImport(@"raylib.dll")]
            public static extern Mesh LoadMesh(string fileName);

            [DllImport(@"raylib.dll")]
            public static extern void UnloadMesh(out Mesh mesh);

            [DllImport(@"raylib.dll")]
            public static extern void ExportMesh(string fileName, Mesh mesh);

            // manipulation
            [DllImport(@"raylib.dll")]
            public static extern BoundingBox MeshBoundingBox(Mesh mesh);

            [DllImport(@"raylib.dll")]
            public static extern void MeshTangents(out Mesh mesh);

            [DllImport(@"raylib.dll")]
            public static extern void MeshBinormals(out Mesh mesh);

            // mesh generation
            [DllImport(@"raylib.dll")]
            public static extern Mesh GenMeshPlane(float width, float length, int resX, int resZ);

            [DllImport(@"raylib.dll")]
            public static extern Mesh GenMeshCube(float width, float height, float length);

            [DllImport(@"raylib.dll")]
            public static extern Mesh GenMeshSphere(float radius, int rings, int slices);

            [DllImport(@"raylib.dll")]
            public static extern Mesh GenMeshHemiSphere(float radius, int rings, int slices);

            [DllImport(@"raylib.dll")]
            public static extern Mesh GenMeshCylinder(float radius, float height, int slices);

            [DllImport(@"raylib.dll")]
            public static extern Mesh GenMeshTorus(float radius, float size, int radSeg, int sides);

            [DllImport(@"raylib.dll")]
            public static extern Mesh GenMeshKnot(float radius, float size, int radSeg, int sides);

            [DllImport(@"raylib.dll")]
            public static extern Mesh GenMeshHeightmap(Image heightmap, Vector3 size);

            [DllImport(@"raylib.dll")]
            public static extern Mesh GenMeshCubicmap(Image cubicmap, Vector3 cubeSize);

            // model drawing
            [DllImport(@"raylib.dll")]
            public static extern void DrawModel(Model model, Vector3 position, float scale, Color tint);

            [DllImport(@"raylib.dll")]
            public static extern void DrawModelEx(Model model, Vector3 position, Vector3 rotationAxis,
                float rotationAngle, Vector3 scale, Color tint);

            [DllImport(@"raylib.dll")]
            public static extern void DrawModelWires(Model model, Vector3 position, float scale, Color tint);

            [DllImport(@"raylib.dll")]
            public static extern void DrawModelWiresEx(Model model, Vector3 position, Vector3 rotationAxis,
                float rotationAngle, Vector3 scale, Color tint);

            [DllImport(@"raylib.dll")]
            public static extern void DrawBoundingBox(BoundingBox box, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawBillboard(Camera3D camera, Texture2D texture, Vector3 center, float size,
                Color tint);

            [DllImport(@"raylib.dll")]
            public static extern void DrawBillboardRec(Camera3D camera, Texture2D texture, Rectangle sourceRec,
                Vector3 center, float size, Color tint);

            // collision
            [DllImport(@"raylib.dll")]
            public static extern bool CheckCollisionSpheres(Vector3 centerA, float raduysA, Vector3 centerB,
                float radiusB);

            [DllImport(@"raylib.dll")]
            public static extern bool CheckCollisionBoxes(Vector3 minBBox1, Vector3 maxBBox1, Vector3 minBBox2,
                Vector3 maxBBox2);

            [DllImport(@"raylib.dll")]
            public static extern bool CheckCollisionBoxSphere(Vector3 minBBox, Vector3 maxBBox, Vector3 centerSphere,
                float radiusSphere);

            [DllImport(@"raylib.dll")]
            public static extern bool CheckCollisionRaySphere(Ray ray, Vector3 spherePosition, float sphereRadius);

            [DllImport(@"raylib.dll")]
            public static extern bool CheckCollisionRaySphereEx(Ray ray, Vector3 spherePosition, float sphereRadius,
                Vector3[] collisionPoint);

            [DllImport(@"raylib.dll")]
            public static extern bool CheckCollisionRayBox(Ray ray, Vector3 minBBox, Vector3 maxBBox);

            [DllImport(@"raylib.dll")]
            public static extern RayHitInfo GetColliionRayModel(Ray ray, out Model model);

            [DllImport(@"raylib.dll")]
            public static extern RayHitInfo GetCollisionRayTriangle(Ray ray, Vector3 p1, Vector3 p2, Vector3 p3);

            [DllImport(@"raylib.dll")]
            public static extern RayHitInfo GetCollisionRayGround(Ray ray, float groundHeight);

            // window related
            [DllImport(@"raylib.dll")]
            public static extern void InitWindow(int width, int height, string title);

            [DllImport(@"raylib.dll")]
            public static extern void CloseWindow();

            [DllImport(@"raylib.dll")]
            public static extern bool IsWindowRead();

            [DllImport(@"raylib.dll")]
            public static extern bool WindowShouldClose();

            [DllImport(@"raylib.dll")]
            public static extern bool IsWindowMinimized();

            [DllImport(@"raylib.dll")]
            public static extern void ToggleFullscreen();

            [DllImport(@"raylib.dll")]
            public static extern void SetWindowIcon(Image image);

            [DllImport(@"raylib.dll")]
            public static extern void SetWindowTitle(string title);

            [DllImport(@"raylib.dll")]
            public static extern void SetWindowPosition(int x, int y);

            [DllImport(@"raylib.dll")]
            public static extern void SetWindowMonitor(int monitor);

            [DllImport(@"raylib.dll")]
            public static extern void SetWindowMinSize(int width, int height);

            [DllImport(@"raylib.dll")]
            public static extern void SetWindowSize(int width, int height);

            [DllImport(@"raylib.dll")]
            public static extern int GetScreenWidth();

            [DllImport(@"raylib.dll")]
            public static extern int GetScreenHeight();

            // cursor related
            [DllImport(@"raylib.dll")]
            public static extern void ShowCursor();

            [DllImport(@"raylib.dll")]
            public static extern void HideCursor();

            [DllImport(@"raylib.dll")]
            public static extern bool IsCursorHidden();

            [DllImport(@"raylib.dll")]
            public static extern void EnableCursor();

            [DllImport(@"raylib.dll")]
            public static extern void DisableCursor();

            // drawing related
            [DllImport(@"raylib.dll")]
            public static extern void ClearBackground(Color color);

            [DllImport(@"raylib.dll")]
            public static extern void BeginDrawing();

            [DllImport(@"raylib.dll")]
            public static extern void EndDrawing();

            [DllImport(@"raylib.dll")]
            public static extern void BeginMode2D(Camera2D camera);

            [DllImport(@"raylib.dll")]
            public static extern void EndMode2D();

            [DllImport(@"raylib.dll")]
            public static extern void BeginMode3D(Camera3D camera);

            [DllImport(@"raylib.dll")]
            public static extern void EndMode3D();

            [DllImport(@"raylib.dll")]
            public static extern void BeginTextureMode(RenderTexture2D target);

            [DllImport(@"raylib.dll")]
            public static extern void EndTextureMode();

            // screen-space related
            [DllImport(@"raylib.dll")]
            public static extern Ray GetMouseRay(Vector2 mousePosition, Camera3D camera);

            [DllImport(@"raylib.dll")]
            public static extern Vector2 GetWorldToScreen(Vector3 position, Camera3D camera);

            [DllImport(@"raylib.dll")]
            public static extern Matrix GetCameraMatrix(Camera3D camera);

            // timing related
            [DllImport(@"raylib.dll")]
            public static extern void SetTargetFPS(int fps);

            [DllImport(@"raylib.dll")]
            public static extern int GetFPS();

            [DllImport(@"raylib.dll")]
            public static extern float GetFrameTime();

            [DllImport(@"raylib.dll")]
            public static extern double GetTime();

            // color related
            [DllImport(@"raylib.dll")]
            public static extern int ColorToInt(Color color);

            [DllImport(@"raylib.dll")]
            public static extern Vector4 ColorNormalize(Color color);

            [DllImport(@"raylib.dll")]
            public static extern Vector3 ColorToHSV(Color color);

            [DllImport(@"raylib.dll")]
            public static extern Color GetColor(int hexValue);

            [DllImport(@"raylib.dll")]
            public static extern Color Fade(Color color, float alpha);

            // misc
            [DllImport(@"raylib.dll")]
            public static extern void ShowLogo();

            [DllImport(@"raylib.dll")]
            public static extern void SetConfigFlags(ConfigFlag flag);

            [DllImport(@"raylib.dll")]
            public static extern void SetTraceLog(TraceLogFlag types);

            [DllImport(@"raylib.dll")]
            public static extern void TraceLog(TraceLogFlag logType, string text);

            [DllImport(@"raylib.dll")]
            public static extern void TakeScreenshot(string fileName);

            [DllImport(@"raylib.dll")]
            public static extern int GetRandomValue(int min, int max);

            // file related
            [DllImport(@"raylib.dll")]
            public static extern bool IsFileExtension(string fileName, string ext);

            [DllImport(@"raylib.dll")]
            public static extern string GetExtension(string fileName);

            [DllImport(@"raylib.dll")]
            public static extern string GetFileName(string filePath);

            [DllImport(@"raylib.dll")]
            public static extern string GetDirectoryPath(string fileName);

            [DllImport(@"raylib.dll")]
            public static extern string GetWorkingDirectory();

            [DllImport(@"raylib.dll")]
            public static extern bool ChangeDirectory(string dir);

            [DllImport(@"raylib.dll")]
            public static extern bool IsFileDropped();

            [DllImport(@"raylib.dll")]
            public static extern string[] GetDroppedFiles(out int count);

            [DllImport(@"raylib.dll")]
            public static extern void ClearDroppedFiles();

            // storage related
            [DllImport(@"raylib.dll")]
            public static extern void StorageSaveValue(int position, int value);

            [DllImport(@"raylib.dll")]
            public static extern int StorageLoadValue(int position);

            // input related
            [DllImport(@"raylib.dll")]
            public static extern bool IsKeyPressed(KeyboardKey key);

            [DllImport(@"raylib.dll")]
            public static extern bool IsKeyDown(KeyboardKey key);

            [DllImport(@"raylib.dll")]
            public static extern bool IsKeyReleased(KeyboardKey key);

            [DllImport(@"raylib.dll")]
            public static extern bool IsKeyUp(KeyboardKey key);

            [DllImport(@"raylib.dll")]
            public static extern int GetKeyPressed();

            [DllImport(@"raylib.dll")]
            public static extern void SetExitKey(KeyboardKey key);

            // gamepad related
            [DllImport(@"raylib.dll")]
            public static extern bool IsGamepadAvailable(int gamepad);

            [DllImport(@"raylib.dll")]
            public static extern bool IsGamepadName(int gamepad, string name);

            [DllImport(@"raylib.dll")]
            public static extern string GetGamepadName(int gamepad);

            [DllImport(@"raylib.dll")]
            public static extern bool IsGamepadButtonPressed(int gamepad, int button);

            [DllImport(@"raylib.dll")]
            public static extern bool IsGamepadButtonDown(int gamepad, int button);

            [DllImport(@"raylib.dll")]
            public static extern bool IsGamepadButtonReleased(int gamepad, int button);

            [DllImport(@"raylib.dll")]
            public static extern bool IsGamepadButtonUp(int gamepad, int button);

            [DllImport(@"raylib.dll")]
            public static extern int GetGamepadButtonPressed();

            [DllImport(@"raylib.dll")]
            public static extern int GetGamepadAxisCount(int gamepad);

            [DllImport(@"raylib.dll")]
            public static extern float GetGamepadAxisMovement(int gamepad, int axis);

            // mouse related
            [DllImport(@"raylib.dll")]
            public static extern bool IsMouseButtonPressed(MouseButton button);

            [DllImport(@"raylib.dll")]
            public static extern bool IsMouseButtonDown(MouseButton button);

            [DllImport(@"raylib.dll")]
            public static extern bool IsMouseButtonReleased(MouseButton button);

            [DllImport(@"raylib.dll")]
            public static extern bool IsMouseButtonUp(MouseButton button);

            [DllImport(@"raylib.dll")]
            public static extern int GetMouseX();

            [DllImport(@"raylib.dll")]
            public static extern int GetMouseY();

            [DllImport(@"raylib.dll")]
            public static extern Vector2 GetMousePosition();

            [DllImport(@"raylib.dll")]
            public static extern void SetMousePosition(Vector2 position);

            [DllImport(@"raylib.dll")]
            public static extern int GetMouseWheelMove();

            // touch related
            [DllImport(@"raylib.dll")]
            public static extern int GetTouchX();

            [DllImport(@"raylib.dll")]
            public static extern int GetTouchY();

            [DllImport(@"raylib.dll")]
            public static extern Vector2 GetTouchPosition();

            // gesture related
            [DllImport(@"raylib.dll")]
            public static extern void SetGesturesEnabled(GestureType gestureFlags);

            [DllImport(@"raylib.dll")]
            public static extern bool IsGestureDetected(GestureType gesture);

            [DllImport(@"raylib.dll")]
            public static extern int GetGestureDetected();

            [DllImport(@"raylib.dll")]
            public static extern int GetTouchPointsCount();

            [DllImport(@"raylib.dll")]
            public static extern float GetGestureHoldDuration();

            [DllImport(@"raylib.dll")]
            public static extern Vector2 GetGestureDragVector();

            [DllImport(@"raylib.dll")]
            public static extern float GetGestureDragAngle();

            [DllImport(@"raylib.dll")]
            public static extern Vector2 GetGesturePinchVector();

            [DllImport(@"raylib.dll")]
            public static extern float GetGesturePunchAngle();

            // camera related
            [DllImport(@"raylib.dll")]
            public static extern void SetCameraMode(Camera3D camera, CameraMode mode);

            [DllImport(@"raylib.dll")]
            public static extern void UpdateCamera(out Camera3D camera);

            [DllImport(@"raylib.dll")]
            public static extern void SetCameraPanControl(int panKey);

            [DllImport(@"raylib.dll")]
            public static extern void SetCameraAltControl(int altKey);

            [DllImport(@"raylib.dll")]
            public static extern void SetCameraSmoothZoomControl(int szKey);

            [DllImport(@"raylib.dll")]
            public static extern void SetCameraMoveControls(int frontKey, int backKey, int rightKey, int leftKey,
                int upKey, int downKey);

            // drawing
            [DllImport(@"raylib.dll")]
            public static extern void DrawPixel(int posX, int posY, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawPixelV(Vector2 position, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawLine(int startPosX, int startPosY, int endPosX, int endPosY, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawLineV(Vector2 startPos, Vector2 endPos, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawLineEx(Vector2 startPos, Vector2 endPos, float thick, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawLineBezier(Vector2 startPos, Vector2 endPos, float thick, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawCircle(int centerX, int centerY, float radius, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawCircleGradient(int centerX, int centerY, float radius, Color color1,
                Color color2);

            [DllImport(@"raylib.dll")]
            public static extern void DrawCircleV(Vector2 center, float radius, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawCircleLines(int centerX, int centerY, float radius, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawRectangle(int posX, int posY, int width, int height, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawRectangleV(Vector2 position, Vector2 size, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawRectangleRec(Rectangle rec, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawRectanglePro(Rectangle rec, Vector2 origin, float rotation, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawRectangleGradientV(int posX, int posY, int width, int height, Color color1,
                Color color2);

            [DllImport(@"raylib.dll")]
            public static extern void DrawRectangleGradientH(int posX, int posY, int width, int height, Color color1,
                Color color2);

            [DllImport(@"raylib.dll")]
            public static extern void DrawRectangleGradientEx(Rectangle rec, Color col1, Color col2, Color col3,
                Color col4);

            [DllImport(@"raylib.dll")]
            public static extern void DrawRectangleLines(int posX, int posY, int width, int height, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawRectangleLinesEx(Rectangle rec, int lineThick, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawTriangle(Vector2 v1, Vector2 v2, Vector2 v3, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawTriangleLines(Vector2 v1, Vector2 v2, Vector2 v3, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawPoly(Vector2 center, int sides, float radius, float rotation, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawPolyEx(Vector2[] points, int numPoints, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawPolyExLines(Vector2[] points, int numPoints, Color color);

            // collision
            [DllImport(@"raylib.dll")]
            public static extern bool CheckCollisionRecs(Rectangle rec1, Rectangle rec2);

            [DllImport(@"raylib.dll")]
            public static extern bool CheckCollisionCircles(Vector2 center1, float radius1, Vector2 center2,
                float radius2);

            [DllImport(@"raylib.dll")]
            public static extern bool CheckCollisionCircleRec(Vector2 center, float radius, Rectangle rec);

            [DllImport(@"raylib.dll")]
            public static extern Rectangle GetCollisionRec(Rectangle rec1, Rectangle rec2);

            [DllImport(@"raylib.dll")]
            public static extern bool CheckCollisionPointRec(Vector2 point, Rectangle rec);

            [DllImport(@"raylib.dll")]
            public static extern bool CheckCollisionPointCircle(Vector2 point, Vector2 center, float radius);

            [DllImport(@"raylib.dll")]
            public static extern bool CheckCollisionPointTriangle(Vector2 point, Vector2 p1, Vector2 p2, Vector2 p3);

            // loading
            [DllImport(@"raylib.dll")]
            public static extern Image LoadImage(string fileName);

            [DllImport(@"raylib.dll")]
            public static extern Image LoadImageEx(Color[] pixels, int width, int height);

            [DllImport(@"raylib.dll")]
            public static extern Image LoadImagePro(byte[] data, int width, int height, int format);

            [DllImport(@"raylib.dll")]
            public static extern Image LoadImageRaw(string fileName, int width, int height, int format,
                int headerSize);

            [DllImport(@"raylib.dll")]
            public static extern void ExportImage(string fileName, Image image);

            [DllImport(@"raylib.dll")]
            public static extern Texture2D LoadTexture(string fileName);

            [DllImport(@"raylib.dll")]
            public static extern Texture2D LoadTextureFromImage(Image image);

            [DllImport(@"raylib.dll")]
            public static extern RenderTexture2D LoadRenderTexture(int width, int height);

            [DllImport(@"raylib.dll")]
            public static extern void UnloadImage(Image image);

            [DllImport(@"raylib.dll")]
            public static extern void UnloadTexture(Texture2D texture);

            [DllImport(@"raylib.dll")]
            public static extern void UnloadRenderTexture(RenderTexture2D target);

            [DllImport(@"raylib.dll")]
            public static extern Color[] GetImageData(Image image);

            [DllImport(@"raylib.dll")]
            public static extern Vector4[] GetImageDataNormalized(Image image);

            [DllImport(@"raylib.dll")]
            public static extern int GetPixelDataSize(int width, int height, int format);

            [DllImport(@"raylib.dll")]
            public static extern Image GetTextureData(Texture2D texture);

            [DllImport(@"raylib.dll")]
            public static extern void UpdateTexture(Texture2D texture, byte[] pixels);

            // image draw
            [DllImport(@"raylib.dll")]
            public static extern Image ImageCopy(Image image);

            [DllImport(@"raylib.dll")]
            public static extern void ImageToPOT(out Image image, Color fillColor);

            [DllImport(@"raylib.dll")]
            public static extern void ImageFormat(out Image image, int newFormat);

            [DllImport(@"raylib.dll")]
            public static extern void ImageAlphaMask(out Image image, Image alphaMask);

            [DllImport(@"raylib.dll")]
            public static extern void ImageAlphaClear(out Image image, Color color, float threshold);

            [DllImport(@"raylib.dll")]
            public static extern void ImageAlphaCrop(out Image image, float threshold);

            [DllImport(@"raylib.dll")]
            public static extern void ImageCrop(out Image image, Rectangle crop);

            [DllImport(@"raylib.dll")]
            public static extern void ImageResize(out Image image, int newWidth, int newHeight);

            [DllImport(@"raylib.dll")]
            public static extern void ImageResizeNN(out Image image, int newWidth, int newHeight);

            [DllImport(@"raylib.dll")]
            public static extern void ImageResizeCanvas(out Image image, int newWidth, int newHeight, int offsetX,
                int offsetY, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void ImageMipmaps(out Image image);

            [DllImport(@"raylib.dll")]
            public static extern void ImageDither(out Image image, int rBpp, int gBpp, int bBpp, int aBpp);

            [DllImport(@"raylib.dll")]
            public static extern Image ImageText(string text, int fontSize, Color color);

            [DllImport(@"raylib.dll")]
            public static extern Image ImageTextEx(Font font, string text, float fontSize, float spacing,
                Color tint);

            [DllImport(@"raylib.dll")]
            public static extern void ImageDraw(out Image dst, Image src, Rectangle srcRec,
                Rectangle dstRec);

            [DllImport(@"raylib.dll")]
            public static extern void ImageDrawRectangle(out Image image, Vector2 position,
                Rectangle rec, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void ImageDrawText(out Image image, Vector2 position, string text,
                int fontSize, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void ImageDrawTextEx(out Image image, Vector2 position, Font font,
                string text, float fontSize, float spacing, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void ImageFlipVertical(out Image image);

            [DllImport(@"raylib.dll")]
            public static extern void ImageFlipHorizontal(out Image image);

            [DllImport(@"raylib.dll")]
            public static extern void ImageRotateCW(out Image image);

            [DllImport(@"raylib.dll")]
            public static extern void ImageRotateCCW(out Image image);

            [DllImport(@"raylib.dll")]
            public static extern void ImageColorTint(out Image image, Color tint);

            [DllImport(@"raylib.dll")]
            public static extern void ImageColorInvert(out Image image);

            [DllImport(@"raylib.dll")]
            public static extern void ImageColorGrayscale(out Image image);

            [DllImport(@"raylib.dll")]
            public static extern void ImageColorContrast(out Image image, float contrast);

            [DllImport(@"raylib.dll")]
            public static extern void ImageColorBrightness(out Image image, float brightness);

            [DllImport(@"raylib.dll")]
            public static extern void ImageColorReplace(out Image image, Color color, Color replace);

            // image generation
            [DllImport(@"raylib.dll")]
            public static extern Image GenImageColor(int width, int height, Color color);

            [DllImport(@"raylib.dll")]
            public static extern Image GenImageGradientV(int width, int height, Color top,
                Color bottom);

            [DllImport(@"raylib.dll")]
            public static extern Image GenImageGradientH(int width, int height, Color left,
                Color right);

            [DllImport(@"raylib.dll")]
            public static extern Image GenImageGradientRadial(int width, int height, float density, Color inner,
                Color outer);

            [DllImport(@"raylib.dll")]
            public static extern Image GenImageChecked(int width, int height, int checksX, int checksY,
                Color col1, Color col2);

            [DllImport(@"raylib.dll")]
            public static extern Image GenImageWhiteNoise(int width, int height, float factor);

            [DllImport(@"raylib.dll")]
            public static extern Image GenImagePerlinNoise(int width, int height, int offsetX, int offsetY,
                float scale);

            [DllImport(@"raylib.dll")]
            public static extern Image GenImageCellular(int width, int height, int tileSize);

            // 2d config
            [DllImport(@"raylib.dll")]
            public static extern void GenTextureMipmaps(ref Texture2D texture);

            [DllImport(@"raylib.dll")]
            public static extern void SetTextureFilter(Texture2D texture, FilterMode filterMode);

            [DllImport(@"raylib.dll")]
            public static extern void SetTextureWrap(Texture2D texture, WrapMode wrapMode);

            // rendering
            [DllImport(@"raylib.dll")]
            public static extern void DrawTexture(Texture2D texture, int posX, int posY, Color tint);

            [DllImport(@"raylib.dll")]
            public static extern void DrawTextureV(Texture2D texture, Vector2 position, Color tint);

            [DllImport(@"raylib.dll")]
            public static extern void DrawTextureEx(Texture2D texture, Vector2 position, float rotation,
                float scale, Color tint);

            [DllImport(@"raylib.dll")]
            public static extern void DrawTextureRec(Texture2D texture, Rectangle sourceRec,
                Vector2 position, Color tint);

            [DllImport(@"raylib.dll")]
            public static extern void DrawTexturePro(Texture2D texture, Rectangle sourceRec,
                Rectangle destRec, Vector2 origin, float rotation, Color tint);

            [DllImport(@"raylib.dll")]
            public static extern Font GetFontDefault();

            [DllImport(@"raylib.dll")]
            public static extern Font LoadFont(string fileName);

            [DllImport(@"raylib.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Font LoadFontEx(string fileName, int fontSize, int charsCount, [MarshalAs(UnmanagedType.LPArray)] int[] fontChars);

            [DllImport(@"raylib.dll")]
            public static extern CharInfo[] LoadFontData(string fileName, int fontSize, int[] fontChars, int charsCount,
                bool sdf);

            [DllImport(@"raylib.dll")]
            public static extern Image GenImageFontAtlas(CharInfo[] chars, int fontSize, int charsCount, int padding,
                int packMethod);

            [DllImport(@"raylib.dll")]
            public static extern void UnloadFont(Font font);

            [DllImport(@"raylib.dll")]
            public static extern void DrawFPS(int posX, int posY);

            [DllImport(@"raylib.dll")]
            public static extern void DrawText(string text, int posX, int posY, int fontSize, Color color);

            [DllImport(@"raylib.dll")]
            public static extern void DrawTextEx(Font font, string text, Vector2 position, float fontSize,
                float spacing, Color tint);

            [DllImport(@"raylib.dll")]
            public static extern int MeasureText(string text, int fontSize);

            [DllImport(@"raylib.dll")]
            public static extern Vector2 MeasureTextEx(Font font, string text, float fontSize, float spacing);

            [DllImport(@"raylib.dll")]
            public static extern string FormatText(string text, params object[] args);

            [DllImport(@"raylib.dll")]
            public static extern string SubText(string text, int position, int length);

            [DllImport(@"raylib.dll")]
            public static extern int GetGlyphIndex(Font font, int character);
        }
    }
}