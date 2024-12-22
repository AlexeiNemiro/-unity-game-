using NUnit.Framework;
using UnityEngine;

public class ChangeFacingDirectionTests
{
    private GameObject skeletonObject;

    [SetUp]
    public void Setup()
    {
        skeletonObject = new GameObject();
    }

    [Test]
    public void ChangeFacingDirection_FacesLeft()
    {
        Vector3 sourcePosition = new Vector3(1, 0, 0);
        Vector3 targetPosition = new Vector3(0, 0, 0);

        ChangeFacingDirection(skeletonObject.transform, sourcePosition, targetPosition);

        Assert.AreEqual(Quaternion.Euler(0, -180, 0), skeletonObject.transform.rotation);
    }

    [Test]
    public void ChangeFacingDirection_FacesRight()
    {
        Vector3 sourcePosition = new Vector3(0, 0, 0);
        Vector3 targetPosition = new Vector3(1, 0, 0);

        ChangeFacingDirection(skeletonObject.transform, sourcePosition, targetPosition);

        Assert.AreEqual(Quaternion.Euler(0, 0, 0), skeletonObject.transform.rotation);
    }

    private void ChangeFacingDirection(Transform transform, Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(skeletonObject);
    }
}
