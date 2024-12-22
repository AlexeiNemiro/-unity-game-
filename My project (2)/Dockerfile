# Используем базовый образ с установленным Unity
FROM unityci/editor:ubuntu-2022.3.10f1

# Устанавливаем рабочую директорию
WORKDIR /app

# Копируем все файлы проекта в контейнер
COPY . .

# Собираем проект
RUN /opt/Unity/Editor/Unity -quit -batchmode -nographics -logFile /dev/stdout -projectPath /app -buildTarget StandaloneLinux64 -executeMethod BuildScript.Build

# Указываем команду для запуска игры
CMD ["./Builds/Linux64/YourGameExecutable"]
