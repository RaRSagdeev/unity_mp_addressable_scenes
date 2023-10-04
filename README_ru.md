## Демонстрация загрузки Addressable Scenes в мультиплеере

### Как попробовать

#### Серверный билд

1. зайти в File -> Build, выбрать Standalone, включить в билд три сцены:

   1. SampleScene
   2. VeryEmptyScene
   3. AdditivelyLoadedAddressableScene

2. Сбилдить программу под именем "build_s"

#### Клиентский билд

1. Зайти в Window -> Assets -> Addressables -> Groups
2. Нажать Build -> Default build script. Сбилдится Addressable Scene "AdditivelyLoadedAddressableScene"
3. Зайти в File -> Build, выбрать Standalone, включить в билд две сцены:
   1. SampleScene
   2. VeryEmptyScene
4. Сбилдить программу под именем "build_c"

#### Тестирование

1. `cd` в папку с проектом
2. а. для запуска сервера выполнить

MacOS: `build_s.app/Contents/MacOS/networking\ tutorial -mode server -screen-fullscreen 0`

Windows: `<что-то похожее>`

2. b. для запуска клиента выполнить

MacOS: `build_c.app/Contents/MacOS/networking\ tutorial -mode client -screen-fullscreen 0`

Windows: `<что-то похожее>`

3. Зайти в окно с сервером. Удостовериться, что отображаются плоскость и куб - по одному объекту из каждой адитивно загруженной сцены - и капсула, обозначающая подключившегося игрока
4. Зайти в окно с сервером. Удостовериться, что отображается капсула - это игрок, которого заспавнил сервер - и плоскость, простой объект на сцене
5. Из окна клиента нажать кнопку "Go to Addressable scene". Произойдёт затемнение, затем должен появиться куб - это объект сцены AdditivelyLoadedAddressableScene.
6. В окне сервера нажать кнопку "Move" (можно несколько раз). Игрок должен перемещаться в обоих окнах.
7. В окне игрока нажать "Go back to sample scene". Должен произойти переход на сцену SampleScene (первую сцену).
